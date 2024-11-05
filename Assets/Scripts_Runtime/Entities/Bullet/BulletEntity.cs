using System;
using UnityEngine;

namespace NJM {

    public class BulletEntity : MonoBehaviour {

        public int id;
        public int typeID;

        [SerializeField] Rigidbody rb;

        public BulletMod mod;
        public BulletAttributeComponent attrComponent;

        public void Ctor() {
            attrComponent = new BulletAttributeComponent();
        }

        public void Inject(BulletMod mod) {
            this.mod = mod;
        }

    }

}