using System;
using UnityEngine;

namespace NJM {

    public class RoleMod : MonoBehaviour {

        [SerializeField] Animator anim;

        [SerializeField] Transform rend_bodyTF;
        [SerializeField] MeshRenderer rend_head;
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
            // ==== Logic ====
            float angleX = logic_head.transform.localEulerAngles.x;
            angleX -= lookAxis.y;
            if (angleX > 180) {
                angleX -= 360;
            }
            float headUpLimit = -65;
            float headDownLimit = 65;
            angleX = Mathf.Clamp(angleX, headUpLimit, headDownLimit);

            Vector3 rotation = new Vector3(angleX, 0, 0);
            logic_head.transform.localEulerAngles = rotation;

            // Human Like
            if (logic_rightHandTF != null) {
                logic_rightHandTF.transform.localEulerAngles = rotation;
            }

            // ==== Render ====
            Vector3 rendForward = (logic_head.transform.forward + logic_bodyTF.forward) / 2f;
            rend_head.transform.forward = rendForward;

            // Human Like
            if (rend_leftHand != null && rend_rightHand != null) {
                rend_leftHand.transform.forward = rendForward;
                rend_rightHand.transform.forward = rendForward;
            }
        }

        public Vector3 Head_TF_Pos() {
            return logic_head.transform.position;
        }

        public Vector3 Head_TF_Forward() {
            return logic_head.transform.forward;
        }

        // - Animation
        public void Anim_Play_Idle() {
            anim.CrossFade(RoleAnimationConst.IDLE, 0.1f);
        }

        public void Anim_Play_GetHit() {
            anim.CrossFade(RoleAnimationConst.GET_HIT, 0.1f);
        }

    }
}