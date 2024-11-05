using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace NJM {

    // 技能栏
    public class RoleSkillSlotComponent {

        List<SkillSubEntity> skills;

        public RoleSkillSlotComponent() {
            skills = new List<SkillSubEntity>(8);
        }

        public void Add(SkillSubEntity skill) {
            skills.Add(skill);
        }

        public bool TryGetByTypeID(int typeID, out SkillSubEntity result) {
            result = skills.FirstOrDefault(s => s.typeID == typeID);
            return result != null;
        }

        public void Foreach(Action<SkillSubEntity> action) {
            skills.ForEach(action);
        }

        public bool TryGetByCastKey(SkillCastKey castKey, out SkillSubEntity result) {
            result = skills.FirstOrDefault(s => s.castKey == castKey);
            return result != null;
        }

    }

}