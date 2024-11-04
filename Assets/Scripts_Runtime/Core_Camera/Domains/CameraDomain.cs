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
            } else if (followType == CameraFollowType.FollowAndRound) {
                var pos = ApplyFollowAndRound(entity, args, dt);
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

            // Offset
            res += entity.followOffset;

            // TODO: Easing

            return res;

        }

        static Vector3 ApplyFollowAndRound(CameraEntity entity, CameraFollowSingleArgs args, float dt) {

            Vector3 res = Vector3.zero;

            // Current Info
            Vector3 cur_pos = entity.pos;
            Vector3 cur_forward = entity.forward;

            // Target Info
            Vector3 target_pos = args.targetPos;
            Vector3 target_forward = args.targetForward;

            // Offset: X
            Vector3 right = Vector3.Cross(Vector3.up, target_forward);
            target_pos += right * entity.followOffset.x;

            // Offset: Y
            target_pos += Vector3.up * entity.followOffset.y;

            // Offset: Z
            Vector3 finalPos = target_pos + -target_forward * entity.followOffset.z;

            // TODO: Easing

            res = finalPos;

            return res;
        }

    }

}