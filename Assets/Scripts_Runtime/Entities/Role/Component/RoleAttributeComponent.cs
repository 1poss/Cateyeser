using System;
using UnityEngine;

namespace NJM {

    public class RoleAttributeComponent {

        // - Locomotion
        float moveSpeed;
        public float MoveSpeed => moveSpeed;
        public void SetMoveSpeed(float moveSpeed) {
            this.moveSpeed = moveSpeed;
        }

        float jumpForce;
        public float JumpForce => jumpForce;
        public void SetJumpForce(float jumpForce) {
            this.jumpForce = jumpForce;
        }

        float fallingG;
        public float FallingG => fallingG;
        public void SetFallingG(float fallingG) {
            this.fallingG = fallingG;
        }

        float fallingMaxSpeed;
        public float FallingMaxSpeed => fallingMaxSpeed;
        public void SetFallingMaxSpeed(float fallingMaxSpeed) {
            this.fallingMaxSpeed = fallingMaxSpeed;
        }

        // - Shooting
        float fpsAimMultiplier;
        public float FpsAimMultiplier => fpsAimMultiplier;
        public void SetFpsAimMultiplier(float fpsAimMultiplier) {
            this.fpsAimMultiplier = fpsAimMultiplier;
        }

        public RoleAttributeComponent() {}
    }
}