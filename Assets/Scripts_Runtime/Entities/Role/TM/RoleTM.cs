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

        // - Attr: Animation
        public float fsm_deadMaintainSec;
        public float fsm_getHitMaintainSec;

        // - Skill
        public SkillSO[] skillSos;

    }

}