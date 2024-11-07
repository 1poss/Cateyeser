using System;
using UnityEngine;

namespace NJM {

    public class RoleAttributeComponent {

        // - Health
        int hp;
        public int Hp => hp;
        public void SetHp(int hp) {
            this.hp = hp;
        }

        int hpMax;
        public int HpMax => hpMax;
        public void SetHpMax(int hpMax) {
            this.hpMax = hpMax;
        }

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