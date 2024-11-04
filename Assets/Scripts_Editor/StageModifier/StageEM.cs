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

            EditorUtility.SetDirty(so);

        }

    }

}