using System;
using UnityEngine;

namespace NJM.Domains {

    public static class BulletFSMDomain {

        public static void Tick(GameContext ctx, BulletEntity bullet, float fixdt) {

            var fsm = bullet.FSMComponent;
            var status = fsm.Status;
            if (status == BulletFSMStatus.Normal) {
                Normal_Tick(ctx, bullet, fixdt);
            } else if (status == BulletFSMStatus.Done) {
                Done_Tick(ctx, bullet, fixdt);
            } else {
                Debug.LogError($"BulletFSMDomain.Tick: {status.ToString()}");
            }

        }

        #region Normal
        public static void Normal_Enter(GameContext ctx, BulletEntity bullet) {
            bullet.FSMComponent.Normal_Enter();
        }

        static void Normal_Tick(GameContext ctx, BulletEntity bullet, float fixdt) {
            BulletDomain.Fly(ctx, bullet, fixdt);
            BulletDomain.Physics_HitProcess(ctx, bullet, fixdt);
        }
        #endregion

        #region Done
        public static void Done_Enter(GameContext ctx, BulletEntity bullet) {

            // RB - Static
            bullet.RB_MakeStatic();

            var attrCom = bullet.attrComponent;
            bullet.FSMComponent.Done_Enter(attrCom.fsm_doneMaintainSec);

        }

        static void Done_Tick(GameContext ctx, BulletEntity bullet, float fixdt) {
            var fsm = bullet.FSMComponent;
            var model = fsm.DoneStateModel;
            model.maintainTimer -= fixdt;
            if (model.maintainTimer <= 0) {
                BulletDomain.Die_There(ctx, bullet, bullet.TF_Head_Pos());
            }
        }
        #endregion


    }
}