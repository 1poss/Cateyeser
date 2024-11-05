using System;

namespace NJM {

    public enum SkillCancelType {
        NoSkillCasting, // 当前没技能
        EnableCancelTo, // 可以取消到某个技能
        DisableCancel, // 禁止取消
    }
}