using System;
using UnityEngine;

namespace NJM {

    // 一个帧区间
    public class SkillActionModel {

        // ==== Lifecycle ====
        public float maintainSec;
        public float maintainTimer;

        // ==== Effector ====
        // - 发射子弹
        // - Buff
        // - 闪现
        // - 动作
        // - 碰撞盒
        // ...

        // ==== Animation ====
        public string anim_name;

        public SkillActionModel() {}

    }

}