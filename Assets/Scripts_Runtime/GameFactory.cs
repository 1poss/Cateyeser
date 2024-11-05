using System;
using UnityEngine;

namespace NJM {

    public static class GameFactory {

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

            role.id = ctx.idService.PickRoleID();
            role.typeID = tm.typeID;
            role.typeName = tm.typeName;
            role.allyStatus = allyStatus;

            var attrCom = role.AttributeComponent;
            attrCom.SetMoveSpeed(tm.moveSpeed);
            attrCom.SetJumpForce(tm.jumpForce);
            attrCom.SetFallingG(tm.fallingG);
            attrCom.SetFallingMaxSpeed(tm.fallingMaxSpeed);

            attrCom.SetFpsAimMultiplier(tm.fpsAimMultiplier);

            return role;

        }

    }
}