using System;
using UnityEngine;

namespace NJM {

    public class RoleInputComponent {

        // - Move
        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;
        public void MoveAxis_Set(Vector2 value) {
            moveAxis = value;
        }
        public void MoveAxis_Reset() {
            moveAxis = Vector2.zero;
        }

        // - Jump
        bool isJumpDown;
        public bool IsJumpDown => isJumpDown;
        public void IsJumpDown_Set(bool value) {
            isJumpDown = value;
        }
        public void IsJumpDown_Reset() {
            isJumpDown = false;
        }

        // - Look
        Vector2 lookAxis;
        public Vector2 LookAxis => lookAxis;
        public void LookAxis_Set(Vector2 value) {
            lookAxis = value;
        }
        public void LookAxis_Reset() {
            lookAxis = Vector2.zero;
        }

        public RoleInputComponent() { }

    }

}