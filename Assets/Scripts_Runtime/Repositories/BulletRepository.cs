using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace NJM {

    public class BulletRepository {

        Dictionary<int, BulletEntity> all;

        BulletEntity[] tempAll;

        public BulletRepository() {
            all = new Dictionary<int, BulletEntity>();
            tempAll = new BulletEntity[200];
        }

        public void Add(BulletEntity bullet) {
            all.Add(bullet.idSig.id, bullet);
        }

        public void Remove(BulletEntity bullet) {
            all.Remove(bullet.idSig.id);
        }

        public BulletEntity Get(int id) {
            return all[id];
        }

        public int TakeAll(out BulletEntity[] result) {
            if (all.Count > tempAll.Length) {
                tempAll = new BulletEntity[all.Count];
            }
            all.Values.CopyTo(tempAll, 0);
            result = tempAll;
            return all.Count;
        }
    }
}