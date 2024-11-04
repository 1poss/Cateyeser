using System;

namespace NJM {

    public class RoleFSMComponent {

        RoleFSMStatus status;
        public RoleFSMStatus Status => status;

        public RoleFSMComponent() {}

        public void Normal_Enter() {
            status = RoleFSMStatus.Normal;
        }

        public void Die_Enter() {
            status = RoleFSMStatus.Die;
        }

    }
}