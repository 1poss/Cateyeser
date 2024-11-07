using System;
using UnityEngine;

namespace NJM.Domains {

    public static class RoleDomain {

        #region Lifecycle
        public static RoleEntity Spawn(GameContext ctx, int typeID, AllyStatus allyStatus, Vector3 pos, Vector3 face) {
            var role = GameFactory.Role_Create(ctx, typeID, allyStatus, pos, face);

            RoleFSMDomain.Normal_Enter(ctx, role);

            ctx.roleRepository.Add(role);

            return role;
        }

        public static RoleEntity SpawnOwner(GameContext ctx, int typeID, Vector3 pos, Vector3 face) {
            var role = Spawn(ctx, typeID, AllyStatus.Player, pos, face);
            ctx.gameEntity.ownerRoleID = role.idSig.id;

            // Camera: FPS
            {
                Vector3 followOffset = new Vector3(0, 0, 0);
                ctx.cameraCore.Follow_Single_Setup(CameraCore.fpID, CameraFollowType.FollowAndRound, role.TF_Head_Pos(), role.TF_Head_Forward(), followOffset, 100);
            }

            // Camera: TPS
            {
                Vector3 followOffset = new Vector3(0, 2, 6);
                ctx.cameraCore.Follow_Single_Setup(CameraCore.tpID, CameraFollowType.FollowAndRound, role.TF_Head_Pos(), role.TF_Head_Forward(), followOffset, 100);
            }

            return role;
        }

        public static void Unspawn(GameContext ctx, RoleEntity role) {
            ctx.roleRepository.Remove(role);
            GameObject.Destroy(role.gameObject);
        }
        #endregion

        #region Physics
        public static void Physics_ManualTick(GameContext ctx, RoleEntity role, float fixdt) {

            if (role.IsOwner()) {
                Physics_AimProcess(ctx, role);
            }

            Physics_FootProcess(ctx, role, fixdt);
        }

        static RaycastHit[] tmp_aimCheckHits = new RaycastHit[10];
        static void Physics_AimProcess(GameContext ctx, RoleEntity role) {
            // 作用: 检测准星是否击中地面或者敌人
            var mainCam = ctx.cameraCore.MainCam;
            int layer = 1 << LayerConst.GROUND | 1 << LayerConst.ROLE | 1 << LayerConst.BULLET;
            int count = Physics.RaycastNonAlloc(mainCam.transform.position, mainCam.transform.forward, tmp_aimCheckHits, 100, layer);
            if (count > 0) {
                RaycastHit nearestHit = default;
                float nearestDistSqr = float.MaxValue;
                for (int i = 0; i < count; i += 1) {
                    var hit = tmp_aimCheckHits[i];
                    var hitOwner = hit.collider.GetComponentInParent<RoleEntity>();
                    if (hitOwner != null && hitOwner.IsOwner()) {
                        continue;
                    }
                    if (hit.distance * hit.distance < nearestDistSqr) {
                        nearestHit = hit;
                        nearestDistSqr = hit.distance * hit.distance;
                        role.hasAimHitPoint = true;
                        role.aimHitPoint = hit.point;
                    }
                }
            } else {
                role.hasAimHitPoint = false;
            }
        }

        static RaycastHit[] tmp_footCheckHits = new RaycastHit[10];
        static void Physics_FootProcess(GameContext ctx, RoleEntity role, float fixdt) {
            BoxCollider footCollider = role.Mod.logic_foot as BoxCollider;
            int layer = 1 << LayerConst.GROUND;
            int count = Physics.BoxCastNonAlloc(footCollider.transform.position, footCollider.size / 2, Vector3.down, tmp_footCheckHits, Quaternion.identity, 0.01f, layer);
            if (count > 0 && role.MoveComponent.Velocity().y <= 0) {
                RoleDomain.Locomotion_Land(ctx, role);
            }
        }
        #endregion

        #region Locomotion
        public static void Locomotion_Move(GameContext ctx, RoleEntity role, Vector2 moveAxis, float speed) {
            Vector3 moveDir = ctx.cameraCore.Input_GetMoveForwardDir(moveAxis);
            role.Move_HorizontalByVelocity(moveDir, speed);
        }

        public static void Locomotion_Rotate(GameContext ctx, RoleEntity role, Vector2 lookAxis, Vector2 sensitive, float dt) {
            role.Rotate(lookAxis, sensitive, dt);
        }

        public static void Locomotion_Jump(GameContext ctx, RoleEntity role, bool isJumpDown, float jumpForce) {
            bool succ = role.Jump(isJumpDown, jumpForce);
            if (succ) {
                role.InputComponent.IsJumpDown_Reset();
            }
        }

        public static void Locomotion_Falling(GameContext ctx, RoleEntity role, float fallingG, float fallingMaxSpeed, float fixdt) {
            role.Falling(fallingG, fallingMaxSpeed, fixdt);
        }

        static void Locomotion_Land(GameContext ctx, RoleEntity role) {
            role.Land();
        }
        #endregion

        #region Combat
        public static void Skill_TryCast(GameContext ctx, RoleEntity role) {
            var inputCom = role.InputComponent;
            SkillCastKey castKey = SkillCastKey.None;
            if (inputCom.MeleeAxis > 0) {
                castKey = SkillCastKey.Melee;
            } else if (inputCom.Skill1Axis > 0) {
                castKey = SkillCastKey.Skill1;
            } else if (inputCom.Skill2Axis > 0) {
                castKey = SkillCastKey.Skill2;
            } else if (inputCom.Skill3Axis > 0) {
                castKey = SkillCastKey.Skill3;
            }

            // 找到要放的技能
            var skillSlot = role.SkillSlotComponent;
            bool has = skillSlot.TryGetByCastKey(castKey, out var skill);
            if (!has) {
                // 不存在该技能
                return;
            }

            // 检查技能是否可以释放
            if (!Skill_IsAllowCast(ctx, role, skill)) {
                return;
            }

            // 清空指令缓冲
            if (castKey == SkillCastKey.Melee) {
                inputCom.MeleeAxis_Reset();
            } else if (castKey == SkillCastKey.Skill1) {
                inputCom.Skill1Axis_Reset();
            } else if (castKey == SkillCastKey.Skill2) {
                inputCom.Skill2Axis_Reset();
            } else if (castKey == SkillCastKey.Skill3) {
                inputCom.Skill3Axis_Reset();
            }

            // Cancel
            ref SkillSubEntity nextSkill = ref skill;
            SkillCancelType cancelType = Skill_TryCancel(ctx, role, ref nextSkill);
            if (cancelType == SkillCancelType.DisableCancel) {
                // 禁止取消
                return;
            }

            // 释放技能
            Skill_Cast(ctx, role, skill);

        }

        public static SkillCancelType Skill_TryCancel(GameContext ctx, RoleEntity role, ref SkillSubEntity nextSkill) {
            var skillStateCom = role.SkillStateComponent;
            var currentAction = skillStateCom.GetCurrentAction();
            if (currentAction == null) {
                return SkillCancelType.NoSkillCasting;
            }

            // TODO: Cancel / Combo

            return SkillCancelType.DisableCancel;
        }

        public static bool Skill_IsAllowCast(GameContext ctx, RoleEntity role, SkillSubEntity skill) {
            // TODO: cd, mp, grounded, etc
            return true;
        }

        public static void Skill_Cast(GameContext ctx, RoleEntity role, SkillSubEntity skill) {
            Debug.Log($"RoleDomain.Skill_Cast {role.typeName} {skill.typeName}");
            var skillStateCom = role.SkillStateComponent;
            skillStateCom.CastBegin(skill);
        }

        public static void SkillState_Action_Execute(GameContext ctx, RoleEntity role, float fixdt) {

            var skillStateCom = role.SkillStateComponent;

            var currentAction = skillStateCom.GetCurrentAction();
            if (currentAction == null) {
                return;
            }

            ref int currentIndex = ref skillStateCom.currentSkillActionIndex;
            ref int lastIndex = ref skillStateCom.lastSkillActionIndex;

            if (currentIndex != lastIndex) {
                // - Enter Action
                Skill_Action_Act(ctx, role, skillStateCom.currentSkill, currentAction);
                lastIndex = currentIndex;
            } else {
                // - Execute
                currentAction.maintainTimer -= fixdt;
                if (currentAction.maintainTimer <= 0) {
                    // - Exit
                    currentIndex += 1;
                }
            }

        }

        static void Skill_Action_Act(GameContext ctx, RoleEntity role, SkillSubEntity skill, SkillActionModel action) {
            // - Shoot Bullet
            if (action.hasShootBullet) {
                Debug.Log($"RoleDomain.Skill_Action_Execute ShootBullet: {action.shootBulletSO.tm.typeName}");
                Vector3 fwd;
                if (role.hasAimHitPoint) {
                    fwd = role.aimHitPoint - role.Mod.logic_muzzle.position;
                    fwd.Normalize();
                } else {
                    fwd = role.Mod.logic_muzzle.transform.forward;
                }
                var bullet = BulletDomain.Spawn(ctx, action.shootBulletSO.tm.typeID, role.allyStatus, role.Mod.logic_muzzle.position, fwd);
                bullet.parentIDSig = role.idSig;
            }
        }
        #endregion

        #region GetHit
        public static void GetHit(GameContext ctx, RoleEntity role, int dmg) {
            var victimAttrCom = role.AttributeComponent;
            int finalHp = victimAttrCom.Hp - dmg;
            if (finalHp <= 0) {
                finalHp = 0;
                RoleFSMDomain.Dead_Enter(ctx, role);
            } else {
                role.Mod.Anim_Play_GetHit();
                RoleDomain.Attr_SetHp(ctx, role, finalHp);
            }
        }
        #endregion

        #region Attribute
        public static void Attr_SetHp(GameContext ctx, RoleEntity role, int hp) {
            role.AttributeComponent.SetHp(hp);
        }
        #endregion

    }

}