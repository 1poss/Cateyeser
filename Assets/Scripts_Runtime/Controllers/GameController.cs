using System;
using UnityEngine;
using NJM.Domains;

namespace NJM.Controllers {

    public static class GameController {

        public static void Enter_FirstEnter(GameContext ctx, StageSignature stageSignature) {

            // Stage
            StageDomain.Spawn(ctx, stageSignature);

            // Role
            const int OWNER_TYPEID = 1;
            var role = RoleDomain.SpawnOwner(ctx, OWNER_TYPEID, new Vector3(5, 5, 5), Vector3.forward);

            // Camera

            // UI
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            ctx.uiApp.Panel_AimIndicator_Open();

        }

        public static void PreTick(GameContext ctx, float dt) {
            // Bake Input
            var input = ctx.inputCore;
            ctx.gameEntity.isFPSAiming = input.IsFPSAiming;

            var owner = ctx.Role_Owner();
            if (owner != null) {
                var inputComponent = owner.InputComponent;
                inputComponent.MoveAxis_Set(input.MoveAxis);
                inputComponent.LookAxis_Set(input.LookAxis);
                inputComponent.IsJumpDown_Set(input.IsJumpDown);
                inputComponent.IsFPSAiming_Set(input.IsFPSAiming);
                inputComponent.MeleeAxis_Set(input.MeleeAxis);
                inputComponent.Skill1Axis_Set(input.Skill1Axis);
                inputComponent.Skill2Axis_Set(input.Skill2Axis);
                inputComponent.Skill3Axis_Set(input.Skill3Axis);
            }
        }

        public static void FixTick(GameContext ctx, float fixdt) {

            var input = ctx.inputCore;
            var owner = ctx.Role_Owner();

            // - Role
            if (owner != null) {
                RoleFSMDomain.Tick(ctx, owner, fixdt);
            }

            // - Bullet
            int bulletCount = ctx.bulletRepository.TakeAll(out var bullets);
            for (int i = 0; i < bulletCount; i += 1) {
                var bullet = bullets[i];
                BulletDomain.Fly(ctx, bullet, fixdt);
                BulletDomain.Physics_HitProcess(ctx, bullet, fixdt);
            }

            Physics.Simulate(fixdt);

        }

        public static void LateTick(GameContext ctx, float dt) {

            var input = ctx.inputCore;
            var game = ctx.gameEntity;

            var owner = ctx.Role_Owner();
            if (owner != null) {
                CameraDomain.FollowOwner(ctx, owner, dt);
            }

        }

    }

}