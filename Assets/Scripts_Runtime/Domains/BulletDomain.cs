using System;
using UnityEngine;

namespace NJM.Domains {

    public static class BulletDomain {

        public static BulletEntity Spawn(GameContext ctx, int typeID, AllyStatus allyStatus, Vector3 pos, Vector3 fwd) {
            var bullet = GameFactory.Bullet_Create(ctx, typeID, allyStatus, pos, fwd);

            ctx.bulletRepository.Add(bullet);

            return bullet;
        }
    }
}