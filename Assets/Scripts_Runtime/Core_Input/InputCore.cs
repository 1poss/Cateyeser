using System;
using UnityEngine;
using UnityEngine.InputSystem;
using NJM.CoreInput.Internal;

namespace NJM {

    public class InputCore {

        InputActions inputActions;

        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;

        Vector2 lookAxis;
        public Vector2 LookAxis => lookAxis;

        public InputCore() {
            inputActions = new InputActions();
        }

        public void Init() {
            inputActions.Enable();
        }

        public void Tick(float dt) {
            {
                moveAxis = inputActions.Player.Move.ReadValue<Vector2>();
            }

            {
                lookAxis = inputActions.Player.Look.ReadValue<Vector2>();
            }
        }

    }

}