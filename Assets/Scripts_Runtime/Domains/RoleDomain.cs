using System;
using UnityEngine;

namespace NJM.Domains {

    public static class RoleDomain {

        public static RoleEntity Spawn(GameContext ctx, int typeID, Vector3 pos, Vector3 face) {
            var role = GameFactory.Role_Create(ctx, typeID, pos, face);

            ctx.roleRepository.Add(role);

            return role;
        }

        public static void Move(GameContext ctx, RoleEntity role, Vector2 moveAxis, float speed) {
            Vector3 moveDir = ctx.cameraCore.Input_GetMoveForwardDir(moveAxis);
            role.Move_HorizontalByVelocity(moveDir, speed);
        }

    }

}