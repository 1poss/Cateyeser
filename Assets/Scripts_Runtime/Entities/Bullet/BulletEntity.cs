using System;
using UnityEngine;

namespace NJM {

    public class BulletEntity : MonoBehaviour {

        public IDSignature idSig;
        public IDSignature parentIDSig;
        public int typeID;
        public AllyStatus allyStatus;

        [SerializeField] Rigidbody rb;

        public BulletMod mod;
        public BulletAttributeComponent attrComponent;

        BulletFSMComponent fsmComponent;
        public BulletFSMComponent FSMComponent => fsmComponent;

        public BulletEffectorModel effectorModel;

        public Vector3 originPos;
        public Vector3 originForward;

        public void Ctor() {
            attrComponent = new BulletAttributeComponent();
            fsmComponent = new BulletFSMComponent();
        }

        public void Inject(BulletMod mod) {
            this.mod = mod;
        }

        public void Move(Vector3 dir, float moveSpeed) {
            rb.linearVelocity = dir * moveSpeed;
            // Rotate
            transform.rotation = Quaternion.LookRotation(dir);
        }

        public void RB_MakeStatic() {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        public void TF_Set_Pos(Vector3 pos) {
            transform.position = pos;
        }

        public Vector3 TF_Head_Pos() {
            return transform.position;
        }

        public Vector3 TF_Head_Fwd() {
            return transform.forward;
        }

    }

}