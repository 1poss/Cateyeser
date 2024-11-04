using System;
using UnityEngine;
using NJM.Domains;

namespace NJM.Controllers {

    public static class GameController {

        public static void Enter(GameContext ctx) {
            // Role
            var role = RoleDomain.SpawnOwner(ctx, 1, new Vector3(5, 5, 5), Vector3.forward);

            // Camera

            // UI
            ctx.uiApp.Panel_AimIndicator_Open();

        }

        public static void PreTick(GameContext ctx, float dt) {
            // Bake Input
            var input = ctx.inputCore;
            ctx.gameEntity.isFPSAiming = input.IsFPSAiming;
        }

        public static void FixTick(GameContext ctx, float fixdt) {

            var owner = ctx.Role_Owner();
            var input = ctx.inputCore;
            if (owner != null) {
                RoleDomain.Physics_ManualTick(ctx, owner, fixdt);
                RoleDomain.Locomotion_Move(ctx, owner, input.MoveAxis, 5);
                RoleDomain.Locomotion_Rotate(ctx, owner, input.LookAxis, new Vector2(180, 90), fixdt);
                RoleDomain.Locomotion_Jump(ctx, owner, input.IsJumpDown);
                RoleDomain.Locomotion_Falling(ctx, owner, fixdt);
            }

            Physics.Simulate(fixdt);

        }

        public static void LateTick(GameContext ctx, float dt) {

            var input = ctx.inputCore;
            var game = ctx.gameEntity;

            var owner = ctx.Role_Owner();
            if (owner != null) {

                if (game.isFPSAiming) {
                    // Camera: FPS
                    CameraFollowSingleArgs args;
                    args.targetPos = owner.TF_Pos();
                    args.targetForward = owner.TF_Forward();
                    _ = ctx.cameraCore.Follow_Tick(CameraCore.fpID, args, dt);
                } else {
                    // Camera: TPS
                    CameraFollowSingleArgs args;
                    args.targetPos = owner.TF_Pos();
                    args.targetForward = owner.TF_Forward();
                    _ = ctx.cameraCore.Follow_Tick(CameraCore.tpID, args, dt);
                }

            }

        }

    }

}