using System;
using UnityEngine;
using NJM.ApplicationUI.Internal;

namespace NJM {

    public class UIApp : MonoBehaviour {

        [SerializeField] Canvas canvas_screen;
        [SerializeField] Canvas canvas_worldHUD;
        [SerializeField] Transform canvas_worldFake;

        UIContext ctx;

        public void Ctor() {
            ctx = new UIContext();
        }

        public void Inject(AssetsCore assetsCore) {
            ctx.Inject(assetsCore, canvas_screen, canvas_worldHUD, canvas_worldFake);
        }

        #region Generic
        T Open<T>(Transform tf) where T : MonoBehaviour {
            string name = typeof(T).Name;
            var prefab = ctx.assetsCore.UI_GetPanel(name);
            var panel = GameObject.Instantiate(prefab, tf).GetComponent<T>();
            return panel;
        }
        #endregion

        #region Panel_AimIndicator
        public void Panel_AimIndicator_Open() {
            var panel = ctx.panel_AimIndicator;
            if (panel == null) {
                panel = Open<Panel_AimIndicator>(ctx.canvas_screen.transform);
                panel.Ctor();
            }
        }

        public void Panel_AimIndicator_Close() {

        }
        #endregion

    }

}