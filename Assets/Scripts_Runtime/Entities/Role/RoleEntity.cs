using System;
using UnityEngine;

namespace NJM {

    public class RoleEntity : MonoBehaviour {

        public int id;

        [SerializeField] Rigidbody rb;

        [SerializeField] RoleMod mod;

        public void Ctor() {
            mod.Ctor();
        }

        public Vector3 TF_Pos() {
            return mod.Head_TF_Pos();
        }

        public Vector3 TF_Forward() {
            return mod.Head_TF_Forward();
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