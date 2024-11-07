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

            // - Basic
            tm.stageSignature = stageSignature;

            // - Role Spawner
            {
                var roleSpawnerEMs = transform.GetComponentsInChildren<RoleSpawnerEM>();
                RoleSpawnerTM[] spawners = new RoleSpawnerTM[roleSpawnerEMs.Length];
                for (int i = 0; i < roleSpawnerEMs.Length; i += 1) {
                    var roleSpawnerEM = roleSpawnerEMs[i];
                    roleSpawnerEM.Bake();
                    RoleSpawnerTM spawner = new RoleSpawnerTM();
                    spawner.so = roleSpawnerEM.spawnerTM.so;
                    spawner.pos = roleSpawnerEM.transform.position;
                    spawner.forward = roleSpawnerEM.transform.forward;
                    spawners[i] = spawner;
                }
                tm.roleSpawners = spawners;
            }

            // - Terrain Spawner
            {
                var terrains = transform.GetComponentsInChildren<Terrain>();
                TerrainSpawnerTM[] spawners = new TerrainSpawnerTM[terrains.Length];
                for (int i = 0; i < terrains.Length; i += 1) {
                    var terrain = terrains[i];
                    TerrainSpawnerTM spawner = new TerrainSpawnerTM();
                    spawner.pos = terrain.transform.position;
                    spawner.terrainData = terrain.terrainData;
                    spawners[i] = spawner;
                }
                tm.terrainSpawners = spawners;
            }

            EditorUtility.SetDirty(so);

        }

    }

}