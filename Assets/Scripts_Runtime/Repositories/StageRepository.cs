using System;
using System.Collections.Generic;
using UnityEngine;

namespace NJM {

    public class StageRepository {

        Dictionary<StageSignature, StageEntity> all;

        StageEntity[] tempAll;

        public StageRepository() {
            all = new Dictionary<StageSignature, StageEntity>();
            tempAll = new StageEntity[16];
        }

        public void Add(StageEntity entity) {
            all.Add(entity.stageSignature, entity);
        }

        public StageEntity Get(StageSignature signature) {
            return all[signature];
        }

        public bool Contains(StageSignature signature) {
            return all.ContainsKey(signature);
        }

        public void Remove(StageSignature signature) {
            all.Remove(signature);
        }

        public int TakeAll(out StageEntity[] result) {
            if (all.Count > tempAll.Length) {
                tempAll = new StageEntity[all.Count];
            }
            all.Values.CopyTo(tempAll, 0);
            result = tempAll;
            return all.Count;
        }

    }
}