using System;
using UnityEngine;

namespace NJM.Template {

    [Serializable]
    public struct SkillActionTM {

        // ==== Lifecycle ====
        public float maintainSec;
        // interval

        // ==== Effector ====
        // - 发射子弹
        public bool hasShootBullet;
        public BulletSO shootBulletSO;

        // - Buff
        // - 闪现
        // - 动作
        // - 碰撞盒
        // ...

        // ==== Animation ====
        public string anim_name;

    }

}