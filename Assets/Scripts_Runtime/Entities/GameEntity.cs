using System;

namespace NJM {

    public class GameEntity {

        // ==== Owner ====
        public int ownerRoleID;

        // ==== Camera ====
        public int firstPersonCameraID;
        public int thirdPersonCameraID;

        public GameEntity() {
            ownerRoleID = 0;
            firstPersonCameraID = 1;
            thirdPersonCameraID = 2;
        }

    }

}