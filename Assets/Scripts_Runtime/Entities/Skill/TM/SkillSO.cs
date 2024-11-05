using System;
using UnityEngine;

namespace NJM.Template {

    [CreateAssetMenu(fileName = "So_Skill_", menuName = "NJM/Skill", order = 1)]
    public class SkillSO : ScriptableObject {
        public SkillTM tm;
    }
}