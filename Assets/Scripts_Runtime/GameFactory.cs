using System;
using UnityEngine;
using NJM.Template;

namespace NJM {

    public static class GameFactory {

        #region Stage
        public static StageEntity Stage_Create(GameContext ctx, StageSignature stageSignature) {
            bool has = ctx.assetsCore.So_Stage_TryGet(stageSignature, out var so);
            if (!has) {
                Debug.LogError($"StageDomain.Spawn: StageSignature not found: {stageSignature}");
                return null;
            }

            var tm = so.tm;

            StageEntity stage = new StageEntity();
            stage.stageSignature = stageSignature;

            // - Terrain
            var terrainSpawners = tm.terrainSpawners;
            for (int i = 0; i < terrainSpawners.Length; i += 1) {
                var spawner = terrainSpawners[i];
                var terrain = Terrain.CreateTerrainGameObject(spawner.terrainData);
                terrain.transform.position = spawner.pos;
                terrain.gameObject.layer = LayerConst.GROUND;
                stage.terrains.Add(terrain.GetComponent<Terrain>());
            }

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
            role.id = ctx.idService.PickRoleID();
            role.typeID = tm.typeID;
            role.typeName = tm.typeName;
            role.allyStatus = allyStatus;

            // - Attr
            var attrCom = role.AttributeComponent;
            attrCom.SetMoveSpeed(tm.moveSpeed);
            attrCom.SetJumpForce(tm.jumpForce);
            attrCom.SetFallingG(tm.fallingG);
            attrCom.SetFallingMaxSpeed(tm.fallingMaxSpeed);

            attrCom.SetFpsAimMultiplier(tm.fpsAimMultiplier);

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

            bullet.id = ctx.idService.PickBulletID();
            bullet.typeID = tm.typeID;

            // - Attribute
            var attrCom = bullet.attrComponent;

            attrCom.flyType = tm.flyType;
            attrCom.flySpeed = tm.flySpeed;

            attrCom.dmg = tm.dmg;

            attrCom.maintainSec = tm.maintainSec;
            attrCom.maintainTimer = tm.maintainSec;

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