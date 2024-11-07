using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct StageTM {

        public StageSignature stageSignature;

        // - Role Spawner
        public RoleSpawnerTM[] roleSpawners;

        // - Prop Spawner(Entrance)

        // - Camera Spawner

        // - Terrain Spawner
        public TerrainSpawnerTM[] terrainSpawners;

        // - Light Spawner

    }

}