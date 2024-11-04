using System;
using UnityEngine;
using UnityEngine.InputSystem;
using NJM.CoreInput.Internal;

namespace NJM {

    public class InputCore {

        InputActions inputActions;

        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;

        bool isJumpDown;
        public bool IsJumpDown => isJumpDown;

        Vector2 lookAxis;
        public Vector2 LookAxis => lookAxis;

        bool isFPSAiming;
        public bool IsFPSAiming => isFPSAiming;

        public InputCore() {
            inputActions = new InputActions();
        }

        public void Init() {
            inputActions.Enable();
        }

        public void Tick(float dt) {
            // Move
            {
                moveAxis = inputActions.Player.Move.ReadValue<Vector2>();
            }

            // Jump
            {
                isJumpDown = inputActions.Player.Jump.WasPressedThisFrame();
            }

            // Look
            {
                lookAxis = inputActions.Player.Look.ReadValue<Vector2>();
            }

            // FPSAim
            {
                isFPSAiming = inputActions.Player.FPSAim.ReadValue<float>() > 0;
            }
        }

    }

}