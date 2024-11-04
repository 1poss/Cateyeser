using System;
using UnityEngine;

namespace NJM {

    public static class GameFactory {

        public static RoleEntity Role_Create(GameContext ctx, int typeID, AllyStatus allyStatus, Vector3 pos, Vector3 face) {

            var prefab = ctx.assetsCore.Entity_RolePrefab();
            var go = GameObject.Instantiate(prefab, pos, Quaternion.LookRotation(face));

            bool has = ctx.assetsCore.So_Role_TryGet(typeID, out var so);
            if (!has) {
                Debug.LogError($"So_Role TypeID {typeID} not found");
            }

            var tm = so.tm;

            var role = go.GetComponent<RoleEntity>();
            role.Ctor();

            var mod = GameObject.Instantiate(tm.modPrefab, role.transform);
            mod.Ctor();

            role.Inject(mod);

            role.id = ctx.idService.PickRoleID();
            role.typeID = tm.typeID;
            role.typeName = tm.typeName;
            role.allyStatus = allyStatus;

            var attrCom = role.AttributeComponent;
            attrCom.SetMoveSpeed(tm.moveSpeed);
            attrCom.SetJumpForce(tm.jumpForce);
            attrCom.SetFallingG(tm.fallingG);
            attrCom.SetFallingMaxSpeed(tm.fallingMaxSpeed);

            attrCom.SetFpsAimMultiplier(tm.fpsAimMultiplier);

            return role;

        }

    }
}