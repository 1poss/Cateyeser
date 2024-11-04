using System;
using UnityEngine;

namespace NJM {

    public class RoleEntity : MonoBehaviour {

        public int id;

        [SerializeField] Rigidbody rb;

        [SerializeField] RoleMod mod;
        public RoleInputComponent inputComponent;
        public RoleMoveComponent moveComponent;

        public void Ctor() {

            inputComponent = new RoleInputComponent();
            moveComponent = new RoleMoveComponent();

            mod.Ctor();
            moveComponent.Inject(rb);

        }

        public Vector3 TF_Pos() {
            return mod.Head_TF_Pos();
        }

        public Vector3 TF_Forward() {
            return mod.Head_TF_Forward();
        }

        public void Jump(bool isJumpDown, float jumpForce) {
            if (isJumpDown && moveComponent.isGrounded) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                moveComponent.LeaveGround();
            }
        }

        public void Falling(float g, float maxFallingSpeed, float fixdt) {
            Vector3 vel = rb.linearVelocity;
            vel.y -= g * fixdt;
            vel.y = Mathf.Max(vel.y, -maxFallingSpeed);
            rb.linearVelocity = vel;
        }

        public void Land() {
            moveComponent.Land();
        }

        public void Rotate(Vector2 lookAxis, Vector2 sensitive, float dt) {
            float x = lookAxis.x * sensitive.x * dt;
            float y = lookAxis.y * sensitive.y * dt;
            mod.Body_Rotate(x);
            mod.Head_Rotate(new Vector2(x, y));
        }

        public void Move_HorizontalByVelocity(Vector3 moveDir, float speed) {
            float y = rb.linearVelocity.y;
            Vector3 vel = rb.linearVelocity;
            vel = moveDir * speed;
            vel.y = y;
            rb.linearVelocity = vel;
        }

    }

}