using System;
using UnityEngine;

namespace NJM.CoreCamera.Internal {

    public static class CameraSetupDomain {

        public static void Follow_Single_Setup(CameraContext ctx, CameraEntity entity, CameraFollowType followType, Vector3 targetStartPos, Vector3 targetStartForward, Vector3 followOffset, float followSpeed) {
            entity.pos = targetStartPos;
            entity.forward = targetStartForward;

            entity.followType = followType;
            entity.followOffset = followOffset;
            entity.follow_speed = followSpeed;
        }

        public static void Follow_Single_Offset_Set(CameraContext ctx, CameraEntity entity, Vector3 offset) {
            entity.followOffset = offset;
        }

    }

}