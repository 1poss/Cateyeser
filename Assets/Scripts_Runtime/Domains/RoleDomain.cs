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
            ctx.gameEntity.ownerRoleID = role.id;

            // Camera: FPS
            {
                Vector3 followOffset = new Vector3(0, 0, 0);
                ctx.cameraCore.Follow_Single_Start(CameraCore.fpID, CameraFollowType.OnlyFollow, role.TF_Pos(), role.TF_Forward(), followOffset, 100);
            }

            // Camera: TPS
            {
                Vector3 followOffset = new Vector3(0, 2, 6);
                ctx.cameraCore.Follow_Single_Start(CameraCore.tpID, CameraFollowType.FollowAndRound, role.TF_Pos(), role.TF_Forward(), followOffset, 100);
            }

            return role;
        }
        #endregion

        #region Physics
        public static void Physics_ManualTick(GameContext ctx, RoleEntity role, float fixdt) {
            // Box Cast
            Physics_FootCheck(ctx, role, fixdt);
        }

        static RaycastHit[] tmp_footCheckHits = new RaycastHit[10];
        static void Physics_FootCheck(GameContext ctx, RoleEntity role, float fixdt) {
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

            // 释放技能
            Skill_Cast(ctx, role, skill);

        }

        public static bool Skill_IsAllowCast(GameContext ctx, RoleEntity role, SkillSubEntity skill) {
            // TODO: cd, mp, grounded, etc
            return true;
        }

        public static void Skill_Cast(GameContext ctx, RoleEntity role, SkillSubEntity skill) {
            Debug.Log($"RoleDomain.Skill_Cast {role.typeName} {skill.typeName}");
        }
        #endregion

    }

}