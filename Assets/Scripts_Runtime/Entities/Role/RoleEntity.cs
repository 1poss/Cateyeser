using System;
using UnityEngine;

namespace NJM {

    public class RoleEntity : MonoBehaviour {

        public int id;

        [SerializeField] Rigidbody rb;

        // Entity 根节点, 影响移动和横向旋转, 影响物理
        [SerializeField] Transform root;
        // Entity-Body 子节点, 只影响视角
        [SerializeField] Transform body;

        public void Ctor() {

        }

        public Vector3 TF_Pos() {
            return transform.position;
        }

        public Vector3 TF_Forward() {
            return body.forward;
        }

        public void Rotate(Vector2 lookAxis, Vector2 sensitive, float dt) {
            float x = lookAxis.x * sensitive.x * dt;
            // Horizontal: Root
            root.Rotate(Vector3.up, x);

            // Vertical: Body
            // TODO: Limit
            // TODO: Reverse
            float y = lookAxis.y * sensitive.y * dt;
            body.Rotate(Vector3.right, y);

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