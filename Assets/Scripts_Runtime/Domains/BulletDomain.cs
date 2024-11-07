using System;
using UnityEngine;

namespace NJM.Domains {

    public static class BulletDomain {

        #region Lifecycle
        public static BulletEntity Spawn(GameContext ctx, int typeID, AllyStatus allyStatus, Vector3 pos, Vector3 fwd) {
            var bullet = GameFactory.Bullet_Create(ctx, typeID, allyStatus, pos, fwd);

            ctx.bulletRepository.Add(bullet);

            BulletFSMDomain.Normal_Enter(ctx, bullet);

            return bullet;
        }

        public static void Unspawn(GameContext ctx, BulletEntity bullet) {
            ctx.bulletRepository.Remove(bullet);
            GameObject.Destroy(bullet.gameObject);
        }
        #endregion

        #region Physics
        static RaycastHit[] tmp_hitChecks = new RaycastHit[100];
        public static void Physics_HitProcess(GameContext ctx, BulletEntity bullet, float fixdt) {
            // 注: 所有Bullet以子弹头为坐标
            Vector3 fwd = bullet.TF_Head_Fwd();
            float distance = bullet.attrComponent.flySpeed * fixdt;
            int layer = 1 << LayerConst.BULLET | 1 << LayerConst.ROLE | 1 << LayerConst.GROUND;
            int hitCount = Physics.RaycastNonAlloc(bullet.TF_Head_Pos(), fwd, tmp_hitChecks, distance, layer);
            if (hitCount > 0) {
                for (int i = 0; i < hitCount; i += 1) {
                    if (bullet.attrComponent.restHitTimes <= 0) {
                        break;
                    }
                    var hit = tmp_hitChecks[i];
                    var hitObj = hit.collider.gameObject;
                    var hitBullet = hitObj.GetComponentInParent<BulletEntity>();
                    if (hitBullet != null && hitBullet == bullet) {
                        continue;
                    }

                    // - Hit Ground
                    if (hit.collider.gameObject.layer == LayerConst.GROUND) {
                        Hit_Ground(ctx, bullet, hit);
                    } else if (hit.collider.gameObject.layer == LayerConst.ROLE) {
                        var hitRole = hit.collider.GetComponentInParent<RoleEntity>();
                        if (hitRole != null) {
                            Hit_Role(ctx, bullet, hitRole);
                        }
                    }
                }
            }
        }
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

        #region Hit
        public static void Hit_Ground(GameContext ctx, BulletEntity bullet, RaycastHit hitTarget) {
            bullet.TF_Set_Pos(hitTarget.point);
            var attrCom = bullet.attrComponent;
            if (attrCom.isHitDone) {
                BulletFSMDomain.Done_Enter(ctx, bullet);
            }
        }

        static void Hit_Role(GameContext ctx, BulletEntity bullet, RoleEntity victimRole) {
            var attrCom = bullet.attrComponent;

            // - Damage
            RoleDomain.GetHit(ctx, victimRole, attrCom.dmg);

            // - Hit Times
            attrCom.restHitTimes -= 1;
            if (attrCom.restHitTimes <= 0) {
                Die_There(ctx, bullet, victimRole.TF_Head_Pos());
            }
        }
        #endregion

        #region Die
        public static void Die_There(GameContext ctx, BulletEntity bullet, Vector3 diePos) {

            // SFX

            // VFX

            // - Die Explosion

            Unspawn(ctx, bullet);

        }
        #endregion

    }
}