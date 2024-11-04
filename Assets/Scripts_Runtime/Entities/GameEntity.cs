using System;

namespace NJM {

    public class GameEntity {

        // ==== Owner ====
        public int ownerRoleID;
        public bool isFPSAiming;

        // ==== Camera ====
        public int firstPersonCameraID;
        public int thirdPersonCameraID;

        public GameEntity() {
            ownerRoleID = 0;
            isFPSAiming = false;

            firstPersonCameraID = 1;
            thirdPersonCameraID = 2;
        }

    }

}