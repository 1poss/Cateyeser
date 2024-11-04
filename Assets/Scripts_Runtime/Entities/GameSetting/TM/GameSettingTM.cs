using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct GameSettingTM {

        // 第1关
        public StageSignature startStage;

        // 程序
        public float logoMaintainSec;

        // 相机参数
        public Vector2 fps_rotationSpeed;
        public bool fsp_isInvertX;
        public bool fps_isInvertY;

        public Vector2 tps_rotationSpeed;
        public bool tps_isInvertX;
        public bool tps_isInvertY;

        // UI 参数
        public Vector2Int resolution;
        public FullScreenMode fullScreenMode;

    }

}