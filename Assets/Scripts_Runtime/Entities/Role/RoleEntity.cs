using System;
using UnityEngine;

namespace NJM {

    public class RoleEntity : MonoBehaviour {

        public int id;
        public int typeID;
        public string typeName;
        public AllyStatus allyStatus;

        [SerializeField] Rigidbody rb;

        RoleMod mod;
        public RoleMod Mod => mod;

        RoleInputComponent inputComponent;
        public RoleInputComponent InputComponent => inputComponent;

        RoleMoveComponent moveComponent;
        public RoleMoveComponent MoveComponent => moveComponent;

        RoleAttributeComponent attributeComponent;
        public RoleAttributeComponent AttributeComponent => attributeComponent;

        RoleSkillStateComponent skillStateComponent;
        public RoleSkillStateComponent SkillStateComponent => skillStateComponent;

        RoleSkillSlotComponent skillSlotComponent;
        public RoleSkillSlotComponent SkillSlotComponent => skillSlotComponent;

        RoleFSMComponent fsmComponent;
        public RoleFSMComponent FSMComponent => fsmComponent;

        public bool hasAimHitPoint;
        public Vector3 aimHitPoint;

        public void Ctor() {

            inputComponent = new RoleInputComponent();
            moveComponent = new RoleMoveComponent();
            attributeComponent = new RoleAttributeComponent();
            skillStateComponent = new RoleSkillStateComponent();
            skillSlotComponent = new RoleSkillSlotComponent();

            fsmComponent = new RoleFSMComponent();

            moveComponent.Inject(rb);

        }

        public void Inject(RoleMod mod) {
            this.mod = mod;
        }

        public bool IsOwner() {
            return allyStatus == AllyStatus.Player;
        }

        public Vector3 TF_Head_Pos() {
            return mod.Head_TF_Pos();
        }

        public Vector3 TF_Head_Forward() {
            return mod.Head_TF_Forward();
        }

        public bool Jump(bool isJumpDown, float jumpForce) {
            if (isJumpDown && moveComponent.isGrounded) {
                Vector3 vel = rb.linearVelocity;
                vel.y = jumpForce;
                rb.linearVelocity = vel;
                moveComponent.LeaveGround();
                return true;
            }
            return false;
        }

        public void Falling(float g, float maxFallingSpeed, float fixdt) {
            Vector3 vel = rb.linearVelocity;
            vel.y -= g * fixdt;
            vel.y = Mathf.Max(vel.y, -maxFallingSpeed);
            rb.linearVelocity = vel;
        }

        public void Land() {
            moveComponent.Land();
        }

        public void Rotate(Vector2 lookAxis, Vector2 sensitive, float dt) {
            float x = lookAxis.x * sensitive.x * dt;
            float y = lookAxis.y * sensitive.y * dt;
            mod.Body_Rotate(x);
            mod.Head_Rotate(new Vector2(x, y));
        }

        public void Move_HorizontalByVelocity(Vector3 moveDir, float speed) {
            float y = rb.linearVelocity.y;
            Vector3 vel = rb.linearVelocity;
            vel = moveDir * speed;
            vel.y = y;
            rb.linearVelocity = vel;
        }

    }

}