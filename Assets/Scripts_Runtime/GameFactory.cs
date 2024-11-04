using System;
using UnityEngine;

namespace NJM {

    public static class GameFactory {

        public static RoleEntity Role_Create(GameContext ctx, int typeID, Vector3 pos, Vector3 face) {

            var prefab = ctx.assetsCore.Entity_RolePrefab();
            var go = GameObject.Instantiate(prefab, pos, Quaternion.LookRotation(face));

            var role = go.GetComponent<RoleEntity>();
            role.Ctor();

            role.id = ctx.idService.PickRoleID();

            return role;

        }

    }
}