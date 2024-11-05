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
        const int CONTINUOUS_MAINTAIN_FRAME = 4;
        float meleeAxis;
        int meleeContinuousFrame;
        public float MeleeAxis {
            get {
                if (meleeContinuousFrame > 0) {
                    return 1;
                }
                return meleeAxis;
            }
        }
        public void MeleeAxis_Set(float value) {
            meleeAxis = value;
            if (value != 0) {
                meleeContinuousFrame = CONTINUOUS_MAINTAIN_FRAME;
            } else {
                meleeContinuousFrame--;
            }
        }
        public void MeleeAxis_Reset() {
            meleeAxis = 0;
            meleeContinuousFrame = 0;
        }

        // - Skill1
        float skill1Axis;
        int skill1ContinuousFrame;
        public float Skill1Axis {
            get {
                if (skill1ContinuousFrame > 0) {
                    return 1;
                }
                return skill1Axis;
            }
        }
        public void Skill1Axis_Set(float value) {
            skill1Axis = value;
            if (value != 0) {
                skill1ContinuousFrame = CONTINUOUS_MAINTAIN_FRAME;
            } else {
                skill1ContinuousFrame--;
            }
        }
        public void Skill1Axis_Reset() {
            skill1Axis = 0;
            skill1ContinuousFrame = 0;
        }

        // - Skill2
        float skill2Axis;
        int skill2ContinuousFrame;
        public float Skill2Axis {
            get {
                if (skill2ContinuousFrame > 0) {
                    return 1;
                }
                return skill2Axis;
            }
        }
        public void Skill2Axis_Set(float value) {
            skill2Axis = value;
            if (value != 0) {
                skill2ContinuousFrame = CONTINUOUS_MAINTAIN_FRAME;
            } else {
                skill2ContinuousFrame--;
            }
        }
        public void Skill2Axis_Reset() {
            skill2Axis = 0;
            skill2ContinuousFrame = 0;
        }

        // - Skill3
        float skill3Axis;
        int skill3ContinuousFrame;
        public float Skill3Axis {
            get {
                if (skill3ContinuousFrame > 0) {
                    return 1;
                }
                return skill3Axis;
            }
        }
        public void Skill3Axis_Set(float value) {
            skill3Axis = value;
            if (value != 0) {
                skill3ContinuousFrame = CONTINUOUS_MAINTAIN_FRAME;
            } else {
                skill3ContinuousFrame--;
            }
        }
        public void Skill3Axis_Reset() {
            skill3Axis = 0;
            skill3ContinuousFrame = 0;
        }
        #endregion

        public RoleInputComponent() { }

    }

}