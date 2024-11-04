using System;

namespace NJM.CoreCamera.Internal {

    public class CameraContext {

        public CameraRepository repository;

        public CameraContext() {
            repository = new CameraRepository();
        }

    }

}