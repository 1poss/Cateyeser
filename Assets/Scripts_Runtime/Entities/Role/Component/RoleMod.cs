using System;
using UnityEngine;

namespace NJM {

    public class RoleMod : MonoBehaviour {

        [SerializeField] Transform rend_bodyTF;
        [SerializeField] MeshRenderer rend_head;
        [SerializeField] MeshRenderer rend_bodyMesh;
        [SerializeField] MeshRenderer rend_leftHand;
        [SerializeField] MeshRenderer rend_rightHand;

        [SerializeField] Transform logic_bodyTF;
        [SerializeField] Collider logic_head;
        [SerializeField] Collider logic_bodyBox;
        [SerializeField] Collider logic_leftHand;
        [SerializeField] Collider logic_rightHand;
        [SerializeField] public Collider logic_foot;

        public void Ctor() {

        }

        // 转身
        // 逻辑驱动表现
        public void Body_Rotate(float lookAxisX) {
            // Logic
            logic_bodyTF.transform.Rotate(Vector3.up, lookAxisX);

            // Render
            rend_bodyTF.transform.forward = logic_bodyTF.transform.forward;
        }

        // 转头
        // 注: axisX 不需要动头
        // 如需拆解TPS和FPS, 后续再动
        public void Head_Rotate(Vector2 lookAxis) {
            // Logic
            float angleX = logic_head.transform.localEulerAngles.x;
            angleX -= lookAxis.y;
            if (angleX > 180) {
                angleX -= 360;
            }
            float headUpLimit = -65;
            float headDownLimit = 80;
            angleX = Mathf.Clamp(angleX, headUpLimit, headDownLimit);
            logic_head.transform.localEulerAngles = new Vector3(angleX, 0, 0);

            // Render
            rend_head.transform.forward = logic_head.transform.forward;
        }

        public Vector3 Head_TF_Pos() {
            return logic_head.transform.position;
        }

        public Vector3 Head_TF_Forward() {
            return logic_head.transform.forward;
        }

    }
}