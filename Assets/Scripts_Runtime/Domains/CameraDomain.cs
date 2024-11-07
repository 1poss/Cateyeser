using System;
using UnityEngine;

namespace NJM.Domains {

    public static class CameraDomain {

        public static void FollowOwner(GameContext ctx, RoleEntity owner, float dt) {
            var inputCom = owner.InputComponent;
            if (inputCom.IsFPSAiming) {
                Follow_FPS(ctx, owner, dt);
            } else {
                Follow_TPS(ctx, owner, dt);
            }
        }

        static void Follow_FPS(GameContext ctx, RoleEntity owner, float dt) {

            // TODO: 相机防前穿墙

            const float originDistance = 4;
            float fpsAimMultiplier = owner.AttributeComponent.FpsAimMultiplier;
            float distance = originDistance * fpsAimMultiplier;

            Vector3 headPos = owner.TF_Head_Pos();
            Vector3 headForward = owner.TF_Head_Forward();

            var cam = ctx.cameraCore.MainCam;
            float nearPlane = cam.nearClipPlane;

            if (owner.hasAimHitPoint) {
                Vector3 dir = owner.aimHitPoint - headPos;
                float dirSqr = dir.sqrMagnitude;
                if (dirSqr < distance * distance) {
                    distance = Mathf.Sqrt(dirSqr);
                }
            }

            Vector3 offset = new Vector3(-1f, 0.5f, -distance + nearPlane * cam.aspect * 2);

            CameraFollowSingleArgs args;
            args.targetPos = headPos;
            args.targetForward = headForward;
            ctx.cameraCore.Follow_Single_Offset_Set(CameraCore.tpID, offset);
            _ = ctx.cameraCore.Follow_Tick(CameraCore.tpID, args, dt);

        }

        static void Follow_TPS(GameContext ctx, RoleEntity owner, float dt) {

            // TODO: 相机防后穿墙
            const float originDistance = 3f;

            Vector3 headPos = owner.TF_Head_Pos();
            Vector3 headForward = owner.TF_Head_Forward();

            Vector3 offset = new Vector3(-1f, 0.5f, originDistance);
            ctx.cameraCore.Follow_Single_Offset_Set(CameraCore.tpID, offset);

            CameraFollowSingleArgs args;
            args.targetPos = headPos;
            args.targetForward = headForward;
            _ = ctx.cameraCore.Follow_Tick(CameraCore.tpID, args, dt);
        }

    }

}