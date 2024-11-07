using System;

namespace NJM {

    public class RoleFSMComponent {

        RoleFSMStatus status;
        public RoleFSMStatus Status => status;

        RoleFSMDeadStateModel deadStateModel;
        public RoleFSMDeadStateModel DeadStateModel => deadStateModel;

        RoleFSMGetHitStateModel getHitStateModel;
        public RoleFSMGetHitStateModel GetHitStateModel => getHitStateModel;

        public RoleFSMComponent() {
            status = RoleFSMStatus.None;
            deadStateModel = new RoleFSMDeadStateModel();
            getHitStateModel = new RoleFSMGetHitStateModel();
        }

        public void Normal_Enter() {
            status = RoleFSMStatus.Normal;
        }

        public void GetHit_Enter(float maintainSec) {
            status = RoleFSMStatus.GetHit;
            var model = getHitStateModel;
            model.maintainSec = maintainSec;
            model.maintainTimer = maintainSec;
        }

        public void Die_Enter(float maintainSec) {
            status = RoleFSMStatus.Dead;
            var model = deadStateModel;
            model.maintainSec = maintainSec;
            model.maintainTimer = maintainSec;
        }

    }
}