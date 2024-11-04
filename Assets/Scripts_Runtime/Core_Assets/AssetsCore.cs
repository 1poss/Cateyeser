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

        Dictionary<string, GameObject> panelPrefabs;
        AsyncOperationHandle<IList<GameObject>> panelPrefabHandle;

        public AssetsCore() {
            entityPrefabs = new Dictionary<string, GameObject>();
            panelPrefabs = new Dictionary<string, GameObject>();
        }

        public async Task LoadAll() {
            {
                entityPrefabHandle = Addressables.LoadAssetsAsync<GameObject>(AssetsLabelConst.ENTITY, null);
                await entityPrefabHandle.Task;
                foreach (var prefab in entityPrefabHandle.Result) {
                    entityPrefabs.Add(prefab.name, prefab);
                }
            }

            {
                panelPrefabHandle = Addressables.LoadAssetsAsync<GameObject>(AssetsLabelConst.UI_PANEL, null);
                await panelPrefabHandle.Task;
                foreach (var prefab in panelPrefabHandle.Result) {
                    panelPrefabs.Add(prefab.name, prefab);
                }
            }
        }

        public void UnloadAll() {
            if (entityPrefabHandle.IsValid()) {
                Addressables.Release(entityPrefabHandle);
            }

            if (panelPrefabHandle.IsValid()) {
                Addressables.Release(panelPrefabHandle);
            }
        }

        // ==== Entity ====
        public GameObject Entity_RolePrefab() {
            return entityPrefabs["Entity_Role"];
        }

        // ==== UI ====
        public GameObject UI_GetPanel(string name) {
            return panelPrefabs[name];
        }
    }
}