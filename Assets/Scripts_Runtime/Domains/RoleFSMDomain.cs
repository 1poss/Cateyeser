using System;
using UnityEngine;

namespace NJM.Domains {

    public static class RoleFSMDomain {

        public static void Tick(GameContext ctx, RoleEntity role, float fixdt) {
            var fsm = role.FSMComponent;
            var status = fsm.Status;

            Any_Tick(ctx, role, fixdt);

            if (status == RoleFSMStatus.Normal) {
                Normal_Tick(ctx, role, fixdt);
            } else if (status == RoleFSMStatus.Dead) {
                Dead_Tick(ctx, role, fixdt);
            } else if (status == RoleFSMStatus.GetHit) {
                GetHit_Tick(ctx, role, fixdt);
            } else if (status == RoleFSMStatus.Wait) {

            } else {
                Debug.LogError("RoleFSMDomain.Tick: Unknown FSM status");
            }
        }

        #region Any
        static void Any_Tick(GameContext ctx, RoleEntity role, float fixdt) {
            // Skill: CD
            var skillCom = role.SkillSlotComponent;
            skillCom.Foreach(value => {
                value.cdTimer -= fixdt;
                if (value.cdTimer < 0) {
                    value.cdTimer = 0;
                }
            });

            // TOOD: Buff
        }
        #endregion

        #region Normal
        public static void Normal_Enter(GameContext ctx, RoleEntity role) {
            role.FSMComponent.Normal_Enter();
        }

        static void Normal_Tick(GameContext ctx, RoleEntity role, float fixdt) {
            var game = ctx.gameEntity;
            var inputCom = role.InputComponent;
            var attrCom = role.AttributeComponent;
            var gameSetting = ctx.assetsCore.gameSettingSO.tm;
            Vector2 cameraRotateSensitivity;
            if (game.isFPSAiming) {
                cameraRotateSensitivity = gameSetting.fps_rotationSpeed;
            } else {
                cameraRotateSensitivity = gameSetting.tps_rotationSpeed;
            }
            RoleDomain.Physics_ManualTick(ctx, role, fixdt);
            RoleDomain.Locomotion_Move(ctx, role, inputCom.MoveAxis, attrCom.MoveSpeed);
            RoleDomain.Locomotion_Rotate(ctx, role, inputCom.LookAxis, cameraRotateSensitivity, fixdt);
            RoleDomain.Locomotion_Jump(ctx, role, inputCom.IsJumpDown, attrCom.JumpForce);
            RoleDomain.Locomotion_Falling(ctx, role, attrCom.FallingG, attrCom.FallingMaxSpeed, fixdt);
            RoleDomain.Skill_TryCast(ctx, role);
            RoleDomain.SkillState_Action_Execute(ctx, role, fixdt);
        }
        #endregion

        #region GetHit
        public static void GetHit_Enter(GameContext ctx, RoleEntity role) {
            role.FSMComponent.GetHit_Enter(role.AttributeComponent.FSM_GetHitMaintainSec);

            // - Anim
            role.Mod.Anim_Play_GetHit();
        }

        static void GetHit_Tick(GameContext ctx, RoleEntity role, float fixdt) {
            var state = role.FSMComponent.GetHitStateModel;
            state.maintainTimer -= fixdt;
            if (state.maintainTimer <= 0) {
                Normal_Enter(ctx, role);
            }
        }
        #endregion

        #region Dead
        public static void Dead_Enter(GameContext ctx, RoleEntity role) {
            role.FSMComponent.Die_Enter(role.AttributeComponent.FSM_DeadMaintainSec);

            // - Anim
            role.Mod.Anim_Play_Die();
        }

        static void Dead_Tick(GameContext ctx, RoleEntity role, float fixdt) {
            var state = role.FSMComponent.DeadStateModel;
            state.maintainTimer -= fixdt;
            if (state.maintainTimer <= 0) {
                RoleDomain.Unspawn(ctx, role);
            }
        }
        #endregion
    }
}