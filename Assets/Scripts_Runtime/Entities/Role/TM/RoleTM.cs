using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct RoleTM {

        public int typeID;
        public string typeName;
        public RoleType roleType;

        // - Mod
        public RoleMod modPrefab;

        // - Attr: Health
        public int hp;

        // - Attr: Locomotion
        public float mass;
        public float moveSpeed;
        public float jumpForce;
        public float fallingG;
        public float fallingMaxSpeed;

        // - Attr: Shooting
        public float fpsAimMultiplier;

        // - Skill
        public SkillSO[] skillSos;

    }

}