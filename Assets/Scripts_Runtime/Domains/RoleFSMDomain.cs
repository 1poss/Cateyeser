using System;
using UnityEngine;

namespace NJM.Domains {

    public static class RoleFSMDomain {

        public static void Tick(GameContext ctx, RoleEntity role, float fixdt) {
            var fsm = role.FSMComponent;
            if (fsm.Status == RoleFSMStatus.Normal) {
                Normal_Tick(ctx, role, fixdt);
            } else if (fsm.Status == RoleFSMStatus.Die) {

            } else if (fsm.Status == RoleFSMStatus.Wait) {

            } else {
                Debug.LogError("RoleFSMDomain.Tick: Unknown FSM status");
            }
        }

        public static void Normal_Enter(GameContext ctx, RoleEntity role) {
            role.FSMComponent.Normal_Enter();
        }

        static void Normal_Tick(GameContext ctx, RoleEntity role, float fixdt) {
            var inputCom = role.inputComponent;
            RoleDomain.Physics_ManualTick(ctx, role, fixdt);
            RoleDomain.Locomotion_Move(ctx, role, inputCom.MoveAxis, 5);
            RoleDomain.Locomotion_Rotate(ctx, role, inputCom.LookAxis, new Vector2(180, 90), fixdt);
            RoleDomain.Locomotion_Jump(ctx, role, inputCom.IsJumpDown);
            RoleDomain.Locomotion_Falling(ctx, role, fixdt);
        }
    }
}