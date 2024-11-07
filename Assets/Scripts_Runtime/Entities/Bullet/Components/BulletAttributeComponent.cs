using System;
using UnityEngine;

namespace NJM {

    public class BulletAttributeComponent {

        // - Lifecycle
        public float maintainSec;
        public float maintainTimer;

        public int restHitTimes;

        // - Fly
        public BulletFlyType flyType;
        public float flySpeed;

        // - Hit
        public int dmg;
        
        // - FSM
        public bool isHitDone;
        public float fsm_doneMaintainSec;

        public BulletAttributeComponent() { }

    }

}