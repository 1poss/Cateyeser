using System;
using UnityEngine;
using UnityEditor;
using NJM.Template;

namespace NJM.EditorStage {

    public class RoleSpawnerEM : MonoBehaviour {

        public RoleSpawnerTM spawnerTM;

        [SerializeField] GameObject go;

        void OnEnable() {
            Bake();
        }

        [ContextMenu("Bake")]
        public void Bake() {

            if (spawnerTM.so == null) {
                return;
            }

            spawnerTM.pos = transform.position;
            spawnerTM.forward = transform.forward;

            var tm = spawnerTM.so.tm;

            string name = $"Role_{tm.typeName}";
            if (gameObject.name != name) {
                gameObject.name = name;
            }

            if (go == null) {
                go = Instantiate(tm.modPrefab, transform).gameObject;
            } else if (go != null) {
                if (go.name != tm.modPrefab.name) {
                    DestroyImmediate(go);
                    go = Instantiate(tm.modPrefab, transform).gameObject;
                }
            }

        }

    }

}