using System;
using UnityEngine;
using NJM.Template;

namespace NJM {

    public static class GameFactory {

        #region Stage
        public static StageEntity Stage_Create_PureOne(GameContext ctx, StageSignature stageSignature, out StageTM tm) {
            bool has = ctx.assetsCore.So_Stage_TryGet(stageSignature, out var so);
            if (!has) {
                Debug.LogError($"StageDomain.Spawn: StageSignature not found: {stageSignature}");
                tm = default;
                return null;
            }

            tm = so.tm;

            StageEntity stage = new StageEntity();
            stage.stageSignature = stageSignature;

            return stage;

        }
        #endregion

        #region Role
        public static RoleEntity Role_Create(GameContext ctx, int typeID, AllyStatus allyStatus, Vector3 pos, Vector3 face) {

            var prefab = ctx.assetsCore.Entity_RolePrefab();
            var go = GameObject.Instantiate(prefab, pos, Quaternion.LookRotation(face));

            bool has = ctx.assetsCore.So_Role_TryGet(typeID, out var so);
            if (!has) {
                Debug.LogError($"So_Role TypeID {typeID} not found");
            }

            var tm = so.tm;

            var role = go.GetComponent<RoleEntity>();
            role.Ctor();

            var mod = GameObject.Instantiate(tm.modPrefab, role.transform);
            mod.Ctor();

            role.Inject(mod);

            // - Basic
            role.idSig.entityType = EntityType.Role;
            role.idSig.id = ctx.idService.PickRoleID();
            role.typeID = tm.typeID;
            role.typeName = tm.typeName;
            role.allyStatus = allyStatus;
            role.roleType = tm.roleType;

            // - Attr
            var attrCom = role.AttributeComponent;

            attrCom.SetHp(tm.hp);
            attrCom.SetHpMax(tm.hp);

            role.Mass_Set(tm.mass);
            attrCom.SetMoveSpeed(tm.moveSpeed);
            attrCom.SetJumpForce(tm.jumpForce);
            attrCom.SetFallingG(tm.fallingG);
            attrCom.SetFallingMaxSpeed(tm.fallingMaxSpeed);

            attrCom.SetFpsAimMultiplier(tm.fpsAimMultiplier);

            attrCom.FSM_SetDeadMaintainSec(tm.fsm_deadMaintainSec);
            attrCom.FSM_SetGetHitMaintainSec(tm.fsm_getHitMaintainSec);

            // - Skill
            var skillSlotCom = role.SkillSlotComponent;
            var skillSos = tm.skillSos;
            if (skillSos != null) {
                for (int i = 0; i < skillSos.Length; i += 1) {
                    var skillSo = skillSos[i];
                    var skill = Skill_Create(ctx, skillSo.tm);
                    skillSlotCom.Add(skill);
                }
            }

            return role;

        }
        #endregion

        #region Bullet
        public static BulletEntity Bullet_Create(GameContext ctx, int typeID, AllyStatus allyStatus, Vector3 originPos, Vector3 originForward) {
            bool has = ctx.assetsCore.So_Bullet_TryGet(typeID, out var so);
            if (!has) {
                Debug.LogError($"So_Bullet TypeID {typeID} not found");
            }

            var tm = so.tm;

            var prefab = ctx.assetsCore.Entity_BulletPrefab();
            var go = GameObject.Instantiate(prefab, originPos, Quaternion.LookRotation(originForward));

            var bullet = go.GetComponent<BulletEntity>();
            bullet.Ctor();

            var modPrefab = tm.modPrefab;
            var mod = GameObject.Instantiate(modPrefab, bullet.transform);
            mod.Ctor();

            bullet.Inject(mod);

            // - Basic
            bullet.idSig.entityType = EntityType.Bullet;
            bullet.idSig.id = ctx.idService.PickBulletID();
            bullet.typeID = tm.typeID;
            bullet.allyStatus = allyStatus;

            // - Attribute
            var attrCom = bullet.attrComponent;

            attrCom.maintainSec = tm.maintainSec;
            attrCom.maintainTimer = tm.maintainSec;

            attrCom.restHitTimes = tm.restHitTimes;
            attrCom.isHitDone = tm.isHitStop;
            attrCom.fsm_doneMaintainSec = tm.hitStopMaintainSec;

            attrCom.flyType = tm.flyType;
            attrCom.flySpeed = tm.flySpeed;

            attrCom.dmg = tm.dmg;

            // - Effector
            bullet.effectorModel = tm.effectorModel;

            // - State
            bullet.originPos = originPos;
            bullet.originForward = originForward;

            return bullet;

        }
        #endregion

        #region Skill
        public static SkillSubEntity Skill_Create(GameContext ctx, int typeID) {
            bool has = ctx.assetsCore.So_Skill_TryGet(typeID, out var so);
            if (!has) {
                Debug.LogError($"So_Skill TypeID {typeID} not found");
            }

            var tm = so.tm;
            return Skill_Create(ctx, tm);
        }

        public static SkillSubEntity Skill_Create(GameContext ctx, SkillTM tm) {
            SkillSubEntity skill = new SkillSubEntity();
            skill.id = ctx.idService.PickSkillID();
            skill.typeID = tm.typeID;
            skill.typeName = tm.typeName;

            skill.castKey = tm.castKey;
            skill.isResetCDBySpecialCondition = tm.isResetCDBySpecialCondition;
            skill.cdSec = tm.cdSec;
            skill.cdTimer = 0;

            // - Actions
            var actionTMs = tm.actionTMs;
            if (actionTMs != null) {
                for (int i = 0; i < actionTMs.Length; i += 1) {
                    var actionTM = actionTMs[i];
                    var action = new SkillActionModel();
                    action.FromTM(actionTM);
                    skill.actions.Add(action);
                }
            }

            return skill;
        }
        #endregion
    }
}