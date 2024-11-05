using System;
using UnityEngine;

namespace NJM.Domains {

    public static class BulletDomain {

        #region Lifecycle
        public static BulletEntity Spawn(GameContext ctx, int typeID, AllyStatus allyStatus, Vector3 pos, Vector3 fwd) {
            var bullet = GameFactory.Bullet_Create(ctx, typeID, allyStatus, pos, fwd);

            ctx.bulletRepository.Add(bullet);

            return bullet;
        }
        #endregion

        #region Physics
        #endregion

        #region Locomotion
        public static void Fly(GameContext ctx, BulletEntity bullet, float fixdt) {
            var attrCom = bullet.attrComponent;
            BulletFlyType flyType = attrCom.flyType;
            if (flyType == BulletFlyType.LinearForever) {
                Fly_LinearForever(ctx, bullet, fixdt);
            } else {
                Debug.LogError("BulletDomain.Fly: Unknown flyType");
            }
        }

        static void Fly_LinearForever(GameContext ctx, BulletEntity bullet, float fixdt) {
            var attrCom = bullet.attrComponent;
            var speed = attrCom.flySpeed;
            var dir = bullet.originForward;
            bullet.Move(dir, speed);
        }
        #endregion

    }
}