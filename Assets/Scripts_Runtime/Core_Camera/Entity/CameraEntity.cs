using System;
using UnityEngine;

namespace NJM.CoreCamera.Internal {

    public class CameraEntity {

        public int id;

        public Vector3 pos;
        public Vector3 forward;
        public float fov;

        public CameraFollowType followType;
        public Vector3 followOffset;
        public float follow_speed;

    }

}