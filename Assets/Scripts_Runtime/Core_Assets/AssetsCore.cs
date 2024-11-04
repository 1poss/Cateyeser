using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using NJM.CoreAssets.Internal;

namespace NJM {

    public class AssetsCore {

        Dictionary<string, GameObject> entityPrefabs;
        AsyncOperationHandle<IList<GameObject>> entityPrefabHandle;

        public AssetsCore() {
            entityPrefabs = new Dictionary<string, GameObject>();
        }

        public async Task LoadAll() {
            {
                entityPrefabHandle = Addressables.LoadAssetsAsync<GameObject>(AssetsLabelConst.ENTITY, null);
                await entityPrefabHandle.Task;
                foreach (var prefab in entityPrefabHandle.Result) {
                    entityPrefabs.Add(prefab.name, prefab);
                }
            }
        }

        public void UnloadAll() {
            if (entityPrefabHandle.IsValid()) {
                Addressables.Release(entityPrefabHandle);
            }
        }

        // ==== Entity ====
        public GameObject Entity_RolePrefab() {
            return entityPrefabs["Entity_Role"];
        }
    }
}