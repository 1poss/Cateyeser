using System;
using UnityEngine;

namespace NJM {

    public class RoleInputComponent {

        #region Locomotion
        // - Move
        Vector2 moveAxis;
        public Vector2 MoveAxis => moveAxis;
        public void MoveAxis_Set(Vector2 value) => moveAxis = value;
        public void MoveAxis_Reset() => moveAxis = Vector2.zero;

        // - Jump
        bool isJumpDown;
        public bool IsJumpDown => isJumpDown;
        public void IsJumpDown_Set(bool value) => isJumpDown = value;
        public void IsJumpDown_Reset() => isJumpDown = false;

        // - Look
        Vector2 lookAxis;
        public Vector2 LookAxis => lookAxis;
        public void LookAxis_Set(Vector2 value) => lookAxis = value;
        public void LookAxis_Reset() => lookAxis = Vector2.zero;
        #endregion

        #region Skill
        // - FPSAiming
        bool isFPSAiming;
        public bool IsFPSAiming => isFPSAiming;
        public void IsFPSAiming_Set(bool value) => isFPSAiming = value;
        public void IsFPSAiming_Reset() => isFPSAiming = false;

        // - Melee
        float meleeAxis;
        public float MeleeAxis => meleeAxis;
        public void MeleeAxis_Set(float value) => meleeAxis = value;
        public void MeleeAxis_Reset() => meleeAxis = 0;

        // - Skill1
        float skill1Axis;
        public float Skill1Axis => skill1Axis;
        public void Skill1Axis_Set(float value) => skill1Axis = value;
        public void Skill1Axis_Reset() => skill1Axis = 0;

        // - Skill2
        float skill2Axis;
        public float Skill2Axis => skill2Axis;
        public void Skill2Axis_Set(float value) => skill2Axis = value;
        public void Skill2Axis_Reset() => skill2Axis = 0;

        // - Skill3
        float skill3Axis;
        public float Skill3Axis => skill3Axis;
        public void Skill3Axis_Set(float value) => skill3Axis = value;
        public void Skill3Axis_Reset() => skill3Axis = 0;
        #endregion

        public RoleInputComponent() { }

    }

}