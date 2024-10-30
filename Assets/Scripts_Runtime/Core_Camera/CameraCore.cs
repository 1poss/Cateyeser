using System;
using UnityEngine;

namespace Cateyeser {

    public class CameraCore {

        Camera mainCam;
        public Camera MainCam => mainCam;

        public CameraCore() {}

        public void Init() {
            if (mainCam == null) {
                mainCam = Camera.main;
            }
        }

        public Vector3 GetMoveForwardDir(Vector2 moveAxis) {
            
            moveAxis.Normalize();
            
            Vector3 fwd = mainCam.transform.forward;
            Vector3 right = mainCam.transform.right;
            fwd *= moveAxis.y;
            right *= moveAxis.x;
            Vector3 moveDir = fwd + right;
            moveDir.y = 0;
            moveDir.Normalize();

            return moveDir;
            
        }

    }

}