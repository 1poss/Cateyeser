using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct SkillTM {

        public int typeID;
        public string typeName;

        // - Cast
        public SkillCastKey castKey;
        public bool isResetCDBySpecialCondition;
        public float cdSec;

        // - Actions
        public SkillActionTM[] actionTMs;

    }

}