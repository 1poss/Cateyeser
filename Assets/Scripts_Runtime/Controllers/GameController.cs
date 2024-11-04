using System;
using UnityEngine;
using NJM.Domains;

namespace NJM.Controllers {

    public static class GameController {

        public static void Enter(GameContext ctx) {
            var role = RoleDomain.SpawnOwner(ctx, 1, new Vector3(5, 5, 5), Vector3.forward);
        }

        public static void PreTick(GameContext ctx, float dt) {

        }

        public static void FixTick(GameContext ctx, float fixdt) {

            var owner = ctx.Role_Owner();
            var input = ctx.inputCore;
            if (owner != null) {
                RoleDomain.Move(ctx, owner, input.MoveAxis, 5);
            }

            Physics.Simulate(fixdt);

        }

        public static void LateTick(GameContext ctx, float dt) {

            var owner = ctx.Role_Owner();
            if (owner != null) {
                // Camera
                CameraFollowSingleArgs args;
                args.targetPos = owner.TF_Pos();
                args.targetForward = owner.TF_Forward();
                _ = ctx.cameraCore.Follow_Tick(CameraCore.fpID, args, dt);
            }

        }

    }

}