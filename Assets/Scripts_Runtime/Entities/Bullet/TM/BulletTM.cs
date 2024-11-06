using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct BulletTM {

        public int typeID;
        public string typeName;

        // ==== Mod ====
        public BulletMod modPrefab;

        // ==== Lifecycle ====
        public float maintainSec;

        // ==== Hit ====
        public int restHitTimes;

        // ==== Attribute ====
        public BulletFlyType flyType;
        public float flySpeed;
        public int dmg;

    }

}