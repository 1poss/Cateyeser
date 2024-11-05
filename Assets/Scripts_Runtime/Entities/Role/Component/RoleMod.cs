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
        [SerializeField] Transform logic_rightHandTF;
        [SerializeField] public Collider logic_foot;
        [SerializeField] public Transform logic_gun;
        [SerializeField] public Transform logic_muzzle;

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

            Vector3 rotation = new Vector3(angleX, 0, 0);
            logic_head.transform.localEulerAngles = rotation;
            logic_rightHandTF.transform.localEulerAngles = rotation;

            // Render
            Vector3 rendForward = (logic_head.transform.forward + logic_bodyTF.forward) / 2f;
            rend_head.transform.forward = rendForward;
            rend_leftHand.transform.forward = rendForward;
            rend_rightHand.transform.forward = rendForward;
        }

        public Vector3 Head_TF_Pos() {
            return logic_head.transform.position;
        }

        public Vector3 Head_TF_Forward() {
            return logic_head.transform.forward;
        }

    }
}