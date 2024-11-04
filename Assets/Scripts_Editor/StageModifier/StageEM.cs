using System;
using UnityEngine;
using UnityEditor;
using NJM.Template;

namespace NJM.EditorStage {

    public class StageEM : MonoBehaviour {

        public StageSO so;

        [SerializeField] public StageSignature stageSignature;

        [ContextMenu("Bake")]
        void Bake() {

            if (so == null) {
                Debug.LogError("No StageSO assigned");
                return;
            }

            ref var tm = ref so.tm;

            tm.stageSignature = stageSignature;

            var terrains = transform.GetComponentsInChildren<Terrain>();
            TerrainSpawner[] spawners = new TerrainSpawner[terrains.Length];
            for (int i = 0; i < terrains.Length; i += 1) {
                var terrain = terrains[i];
                TerrainSpawner spawner = new TerrainSpawner();
                spawner.pos = terrain.transform.position;
                spawner.terrainData = terrain.terrainData;
                spawners[i] = spawner;
            }
            tm.terrainSpawners = spawners;

            EditorUtility.SetDirty(so);

        }

    }

}