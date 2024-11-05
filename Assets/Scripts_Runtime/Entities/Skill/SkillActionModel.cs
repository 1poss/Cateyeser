using System;
using UnityEngine;
using NJM.Template;

namespace NJM {

    // 一个帧区间
    public class SkillActionModel {

        // ==== Lifecycle ====
        public float maintainSec;
        public float maintainTimer;

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

        public SkillActionModel() { }

        public void FromTM(SkillActionTM tm) {

            maintainSec = tm.maintainSec;
            maintainTimer = tm.maintainSec;

            hasShootBullet = tm.hasShootBullet;
            shootBulletSO = tm.shootBulletSO;

            anim_name = tm.anim_name;
        }

    }

}