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

        public static void Move(GameContext ctx, RoleEntity role, Vector2 moveAxis, float speed) {
            Vector3 moveDir = ctx.cameraCore.Input_GetMoveForwardDir(moveAxis);
            role.Move_HorizontalByVelocity(moveDir, speed);
        }

        public static void Rotate(GameContext ctx, RoleEntity role, Vector2 lookAxis, Vector2 sensitive, float dt) {
            role.Rotate(lookAxis, sensitive, dt);
        }

    }

}