using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using NJM.Template;
using NJM.CoreAssets.Internal;

namespace NJM {

    public class AssetsCore {

        Dictionary<string, GameObject> entityPrefabs;
        AsyncOperationHandle<IList<GameObject>> entityPrefabHandle;

        Dictionary<int, RoleSO> so_roles;
        AsyncOperationHandle<IList<RoleSO>> so_rolesHandle;

        Dictionary<string, GameObject> panelPrefabs;
        AsyncOperationHandle<IList<GameObject>> panelPrefabHandle;

        public AssetsCore() {
            
            entityPrefabs = new Dictionary<string, GameObject>();
            panelPrefabs = new Dictionary<string, GameObject>();

            so_roles = new Dictionary<int, RoleSO>();

        }

        public async Task LoadAll() {
            {
                // - Entity
                entityPrefabHandle = Addressables.LoadAssetsAsync<GameObject>(AssetsLabelConst.ENTITY, null);
                await entityPrefabHandle.Task;
                foreach (var prefab in entityPrefabHandle.Result) {
                    entityPrefabs.Add(prefab.name, prefab);
                }
            }

            {
                // - UI Panel
                panelPrefabHandle = Addressables.LoadAssetsAsync<GameObject>(AssetsLabelConst.UI_PANEL, null);
                await panelPrefabHandle.Task;
                foreach (var prefab in panelPrefabHandle.Result) {
                    panelPrefabs.Add(prefab.name, prefab);
                }
            }

            {
                // - SO Role
                so_rolesHandle = Addressables.LoadAssetsAsync<RoleSO>(AssetsLabelConst.SO_ROLE, null);
                await so_rolesHandle.Task;
                foreach (var so in so_rolesHandle.Result) {
                    so_roles.Add(so.tm.typeID, so);
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

            if (so_rolesHandle.IsValid()) {
                Addressables.Release(so_rolesHandle);
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

        // ==== So Roles ====
        public bool So_Role_TryGet(int typeID, out RoleSO so) {
            return so_roles.TryGetValue(typeID, out so);
        }

    }
}