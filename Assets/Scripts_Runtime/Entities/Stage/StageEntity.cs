using System;
using System.Collections.Generic;
using UnityEngine;

namespace NJM {

    public class StageEntity {

        public StageSignature stageSignature;

        public List<Terrain> terrains;

        public StageEntity() {
            terrains = new List<Terrain>(4);
        }

    }

}