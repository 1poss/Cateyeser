using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct RoleTM {

        public int typeID;
        public string typeName;

        // - Mod
        public RoleMod modPrefab;

        // - Attr: Locomotion
        public float moveSpeed;
        public float jumpForce;
        public float fallingG;
        public float fallingMaxSpeed;

        // - Attr: Shooting
        public float fpsAimMultiplier;

    }

}