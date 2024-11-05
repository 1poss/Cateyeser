using System;
using UnityEngine;
using UnityEngine.InputSystem;
using NJM.CoreInput.Internal;

namespace NJM {

    public class InputCore {

        InputActions inputActions;

        #region Locomotion
        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;

        bool isJumpDown;
        public bool IsJumpDown => isJumpDown;

        Vector2 lookAxis;
        public Vector2 LookAxis => lookAxis;
        #endregion

        #region Skill
        bool isFPSAiming;
        public bool IsFPSAiming => isFPSAiming;

        float meleeAxis;
        public float MeleeAxis => meleeAxis;

        float skill1Axis;
        public float Skill1Axis => skill1Axis;

        float skill2Axis;
        public float Skill2Axis => skill2Axis;

        float skill3Axis;
        public float Skill3Axis => skill3Axis;
        #endregion

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

            // Melee
            // TODO: 死区
            {
                meleeAxis = inputActions.Player.Melee.ReadValue<float>();
            }

            // Skill1
            {
                skill1Axis = inputActions.Player.Skill1.ReadValue<float>();
            }

            // Skill2
            {
                skill2Axis = inputActions.Player.Skill2.ReadValue<float>();
            }

            // Skill3
            {
                skill3Axis = inputActions.Player.Skill3.ReadValue<float>();
            }
        }

    }

}