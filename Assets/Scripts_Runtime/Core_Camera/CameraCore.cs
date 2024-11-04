using System;
using UnityEngine;
using NJM.CoreCamera;
using NJM.CoreCamera.Internal;

namespace NJM {

    public class CameraCore {

        Camera mainCam;

        CameraContext ctx;

        public const int fpID = 0;
        public const int tpID = 1;

        public CameraCore() {
            ctx = new CameraContext();
        }

        public void Init() {
            mainCam = Camera.main;
            SpawnFPS();
            SpawnTPS();
        }

        void SpawnFPS() {
            SpawnCamera(fpID, mainCam.transform.position, mainCam.transform.forward, mainCam.fieldOfView);
        }

        void SpawnTPS() {
            SpawnCamera(tpID, mainCam.transform.position, mainCam.transform.forward, mainCam.fieldOfView);
        }

        void SpawnCamera(int id, Vector3 pos, Vector3 forward, float fov) {
            CameraEntity entity = new CameraEntity();
            entity.id = id;
            entity.pos = pos;
            entity.forward = forward;
            entity.fov = fov;
            ctx.repository.Add(entity);
        }

        public Vector3 Input_GetMoveForwardDir(Vector2 moveAxis) {

            moveAxis.Normalize();

            Vector3 fwd = mainCam.transform.forward;
            Vector3 right = mainCam.transform.right;
            fwd *= moveAxis.y;
            right *= moveAxis.x;
            Vector3 moveDir = fwd + right;
            moveDir.y = 0;
            moveDir.Normalize();

            return moveDir;

        }

        public CameraResultArgs Follow_Tick(int id, CameraFollowSingleArgs followSingleArgs, float dt) {
            CameraResultArgs res = new CameraResultArgs();

            CameraEntity entity = ctx.repository.Get(id);
            if (entity == null) {
                Debug.LogWarning($"CameraEntity {id} not found");
                return res;
            }

            res = CameraDomain.TickApply(ctx, entity, followSingleArgs, dt);

            // Apply To Main
            mainCam.transform.position = res.pos;
            mainCam.transform.forward = res.forward;

            return res;
        }

        public void Follow_Single_Start(int id, CameraFollowType followType, Vector3 targetStartPos, Vector3 targetStartForward, float followSpeed) {
            CameraEntity entity = ctx.repository.Get(id);
            if (entity == null) {
                Debug.LogWarning($"CameraEntity {id} not found");
                return;
            }
            entity.followType = followType;
            entity.pos = targetStartPos;
            entity.forward = targetStartForward;
            entity.follow_speed = followSpeed;
        }

    }

}