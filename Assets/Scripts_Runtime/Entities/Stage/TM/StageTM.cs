using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct StageTM {

        public StageSignature stageSignature;

        // - Role Spawner

        // - Prop Spawner(Entrance)

        // - Camera Spawner

        // - Terrain Spawner
        public TerrainSpawner[] terrainSpawners;

        // - Light Spawner

    }

}