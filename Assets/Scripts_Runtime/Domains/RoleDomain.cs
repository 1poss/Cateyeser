using System;
using UnityEngine;

namespace NJM.Domains {

    public static class RoleDomain {

        public static RoleEntity Spawn(GameContext ctx, int typeID, Vector3 pos, Vector3 face) {
            var role = GameFactory.Role_Create(ctx, typeID, pos, face);

            ctx.roleRepository.Add(role);

            return role;
        }

        public static RoleEntity SpawnOwner(GameContext ctx, int typeID, Vector3 pos, Vector3 face) {
            var role = Spawn(ctx, typeID, pos, face);
            ctx.gameEntity.ownerRoleID = role.id;

            // Camera: FPS
            {
                Vector3 followOffset = new Vector3(0, 0, 0);
                ctx.cameraCore.Follow_Single_Start(CameraCore.fpID, CameraFollowType.OnlyFollow, role.TF_Pos(), role.TF_Forward(), followOffset, 100);
            }

            // Camera: TPS
            {
                Vector3 followOffset = new Vector3(0, 2, 10);
                ctx.cameraCore.Follow_Single_Start(CameraCore.tpID, CameraFollowType.FollowAndRound, role.TF_Pos(), role.TF_Forward(), followOffset, 100);
            }

            return role;
        }

        public static void Physics_ManualTick(GameContext ctx, RoleEntity role, float fixdt) {
            // Box Cast
            Physics_FootCheck(ctx, role, fixdt);
        }

        static RaycastHit[] tmp_footCheckHits = new RaycastHit[10];
        static void Physics_FootCheck(GameContext ctx, RoleEntity role, float fixdt) {
            BoxCollider footCollider = role.Mod.logic_foot as BoxCollider;
            int layer = 1 << LayerConst.GROUND;
            int count = Physics.BoxCastNonAlloc(footCollider.transform.position, footCollider.size / 2, Vector3.down, tmp_footCheckHits, Quaternion.identity, 0.01f, layer);
            if (count > 0 && role.moveComponent.Velocity().y < 0) {
                RoleDomain.Locomotion_Land(ctx, role);
            }
        }

        public static void Locomotion_Move(GameContext ctx, RoleEntity role, Vector2 moveAxis, float speed) {
            Vector3 moveDir = ctx.cameraCore.Input_GetMoveForwardDir(moveAxis);
            role.Move_HorizontalByVelocity(moveDir, speed);
        }

        public static void Locomotion_Rotate(GameContext ctx, RoleEntity role, Vector2 lookAxis, Vector2 sensitive, float dt) {
            role.Rotate(lookAxis, sensitive, dt);
        }

        public static void Locomotion_Jump(GameContext ctx, RoleEntity role, bool isJumpDown) {
            const float JUMP_FORCE = 100;
            role.Jump(isJumpDown, JUMP_FORCE);
        }

        public static void Locomotion_Falling(GameContext ctx, RoleEntity role, float fixdt) {
            const float G = 9.8f;
            const float MAX_FALLING_SPEED = 40;
            role.Falling(G, MAX_FALLING_SPEED, fixdt);
        }

        static void Locomotion_Land(GameContext ctx, RoleEntity role) {
            role.Land();
        }

    }

}