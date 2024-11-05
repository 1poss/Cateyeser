using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct SkillActionTM {

        // ==== Lifecycle ====
        public float maintainSec;
        // interval

        // ==== Effector ====
        // - 发射弹幕
        // - 发射子弹
        // - Buff
        // - 闪现
        // - 动作
        // - 碰撞盒
        // ...

        // ==== Animation ====
        public string anim_name;

    }

}