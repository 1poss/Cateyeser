using System;
using System.Collections.Generic;
using UnityEngine;

namespace NJM {

    // 当前施法状态
    public class RoleSkillStateComponent {

        public SkillSubEntity currentSkill;
        public int currentSkillActionIndex;
        public int lastSkillActionIndex;

        public RoleSkillStateComponent() { }

        public void CastBegin(SkillSubEntity skill) {
            currentSkill = skill;
            foreach (var action in skill.actions) {
                action.maintainTimer = action.maintainSec;
            }
            currentSkillActionIndex = 0;
            lastSkillActionIndex = -1;
        }

        public SkillActionModel GetCurrentAction() {
            if (currentSkill == null) {
                return null;
            }

            if (currentSkillActionIndex < 0 || currentSkillActionIndex >= currentSkill.actions.Count) {
                return null;
            }

            return currentSkill.actions[currentSkillActionIndex];
        }

    }

}