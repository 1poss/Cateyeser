using System;
using System.Collections.Generic;
using UnityEngine;

namespace NJM {

    public class SkillSubEntity {

        public int id;
        public int typeID;
        public string typeName;

        // ==== Cast ====
        public SkillCastKey castKey;
        public float cdSec;
        public float cdTimer;
        // 2. Cast Condition: MP, CD, Grounded, etc.

        // ==== Actions ====
        public List<SkillActionModel> actions;

        public SkillSubEntity() {
            actions = new List<SkillActionModel>(4);
        }

    }

}