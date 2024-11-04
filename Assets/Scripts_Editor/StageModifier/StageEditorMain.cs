using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace NJM.EditorStage {

    public class StageEditorMain : MonoBehaviour {

        void Awake() {
            var root = transform.root;
            var rootScene = root.gameObject.scene;

            // Destroy all objects in the scene
            foreach (var go in rootScene.GetRootGameObjects()) {
                if (go == root.gameObject) {
                    continue;
                }
                Destroy(go);
            }

            Destroy(this.gameObject);
        }

    }
}
