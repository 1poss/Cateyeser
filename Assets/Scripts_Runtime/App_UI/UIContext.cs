using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NJM.ApplicationUI.Internal {

    public class UIContext {

        public AssetsCore assetsCore;

        public Panel_AimIndicator panel_AimIndicator;

        public Canvas canvas_screen;
        public Canvas canvas_worldHUD;
        public Transform canvas_worldFake;

        public UIContext() { }

        public void Inject(AssetsCore assetsCore, Canvas canvas_screen, Canvas canvas_worldHUD, Transform canvas_worldFake) {
            this.assetsCore = assetsCore;

            this.canvas_screen = canvas_screen;
            this.canvas_worldHUD = canvas_worldHUD;
            this.canvas_worldFake = canvas_worldFake;
        }

    }

}