using System;
using UnityEngine;
using UnityEngine.UI;

namespace NJM.ApplicationUI.Internal {

    public class Panel_AimIndicator : MonoBehaviour {

        [SerializeField] Image aimIndicator;

        public void Ctor() {
            
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void Show() {
            // TODO: FadeIn
            gameObject.SetActive(true);
        }

    }


}