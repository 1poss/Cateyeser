using System;
using UnityEngine;

namespace NJM.CoreCamera.Internal {

    public static class CameraDomain {

        public static CameraResultArgs TickApply(CameraContext ctx, CameraEntity entity, CameraFollowSingleArgs args, float dt) {

            CameraResultArgs res = new CameraResultArgs();
            res.pos = args.targetPos;
            res.forward = args.targetForward;

            CameraFollowType followType = entity.followType;
            if (followType == CameraFollowType.OnlyFollow) {
                var pos = ApplyOnlyFollow(entity, args, dt);
                entity.pos = pos;
                res.pos = pos;
            }

            return res;

        }

        static Vector3 ApplyOnlyFollow(CameraEntity entity, CameraFollowSingleArgs args, float dt) {

            Vector3 res;

            float speed = entity.follow_speed;

            // Cur Pos
            Vector3 cur_pos = entity.pos;

            // Target Pos
            Vector3 target_pos = args.targetPos;

            Vector3 dir = target_pos - cur_pos;
            Vector3 dir_n = dir.normalized;

            float distSqr = dir.sqrMagnitude;
            if (distSqr >= speed * dt) {
                res = cur_pos + dir_n * speed * dt;
            } else {
                res = target_pos;
            }

            return res;

        }

    }

}