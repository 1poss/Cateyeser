using System;
using UnityEngine;

namespace NJM.Domains {

    public static class StageDomain {

        public static StageEntity Spawn(GameContext ctx, StageSignature stageSignature) {

            var stage = GameFactory.Stage_Create_PureOne(ctx, stageSignature, out var tm);

            // - Role
            var roleSpawners = tm.roleSpawners;
            for (int i = 0; i < roleSpawners.Length; i += 1) {
                var spawner = roleSpawners[i];
                var roleTM = spawner.so.tm;
                RoleDomain.Spawn(ctx, roleTM.typeID, spawner.allyStatus, spawner.pos, spawner.forward);
            }

            // - Terrain
            var terrainSpawners = tm.terrainSpawners;
            for (int i = 0; i < terrainSpawners.Length; i += 1) {
                var spawner = terrainSpawners[i];
                var terrain = Terrain.CreateTerrainGameObject(spawner.terrainData);
                terrain.transform.position = spawner.pos;
                terrain.gameObject.layer = LayerConst.GROUND;
                stage.terrains.Add(terrain.GetComponent<Terrain>());
            }

            ctx.stageRepository.Add(stage);
            return stage;
        }

    }

}