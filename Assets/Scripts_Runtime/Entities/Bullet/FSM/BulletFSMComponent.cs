using System;

namespace NJM {

    public class BulletFSMComponent {

        BulletFSMStatus staus;
        public BulletFSMStatus Status => staus;

        BulletFSMDoneStateModel doneStateModel;
        public BulletFSMDoneStateModel DoneStateModel => doneStateModel;

        public BulletFSMComponent() {
            staus = BulletFSMStatus.None;
            doneStateModel = new BulletFSMDoneStateModel();
        }

        public void Normal_Enter() {
            staus = BulletFSMStatus.Normal;
        }

        public void Done_Enter(float maintainSec) {
            staus = BulletFSMStatus.Done;
            var model = doneStateModel;
            model.maintainSec = maintainSec;
            model.maintainTimer = maintainSec;
        }

    }
}