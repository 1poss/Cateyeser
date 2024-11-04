using System;
using UnityEngine;

namespace NJM {

    public class RoleMoveComponent {

        public Rigidbody rb;

        public bool isGrounded;

        public RoleMoveComponent() {
            isGrounded = true;
        }

        public void Inject(Rigidbody rb) {
            this.rb = rb;
        }

        public void ResetVelocity() {
            rb.linearVelocity = Vector3.zero;
        }

        public void Land() {
            isGrounded = true;
        }

        public void LeaveGround() {
            isGrounded = false;
        }

    }
}