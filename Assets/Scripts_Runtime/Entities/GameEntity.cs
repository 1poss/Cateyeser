using System;
using UnityEngine;

namespace NJM {

    public class GameEntity {

        // ==== Owner ====
        public int ownerRoleID;

        // ==== Camera ====
        public int firstPersonCameraID;
        public int thirdPersonCameraID;

        // ==== Aim ====
        public bool isFPSAiming;
        public Vector3 aimHitPoint;

        public GameEntity() {
            ownerRoleID = 0;
            isFPSAiming = false;

            firstPersonCameraID = 1;
            thirdPersonCameraID = 2;
        }

    }

}