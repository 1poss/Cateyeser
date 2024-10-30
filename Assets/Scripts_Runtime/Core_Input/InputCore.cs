using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cateyeser.CoreInput.Internal;

namespace Cateyeser {

    public class InputCore {

        InputActions inputActions;

        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;

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
        }

    }

}