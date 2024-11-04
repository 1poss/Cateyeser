using System;
using System.Collections.Generic;

namespace NJM.CoreCamera.Internal {

    public class CameraRepository {

        Dictionary<int, CameraEntity> all;

        public CameraRepository() {
            all = new Dictionary<int, CameraEntity>();
        }

        public void Add(CameraEntity entity) {
            all.Add(entity.id, entity);
        }

        public CameraEntity Get(int id) {
            return all[id];
        }

    }

}