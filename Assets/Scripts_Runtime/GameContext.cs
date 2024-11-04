using System;

namespace NJM {

    public class GameContext {

        // ==== Unique Entity ====
        public GameEntity gameEntity;
        public ProgramEntity programEntity;

        // ==== Entity Repository ====
        public StageRepository stageRepository;
        public RoleRepository roleRepository;

        // ==== Cores ====
        public InputCore inputCore;
        public AssetsCore assetsCore;
        public CameraCore cameraCore;

        // ==== Applications ====
        public UIApp uiApp;

        // ==== Services ====
        public IDService idService;

        public GameContext() {

            gameEntity = new GameEntity();
            programEntity = new ProgramEntity();

            stageRepository = new StageRepository();
            roleRepository = new RoleRepository();

            inputCore = new InputCore();
            assetsCore = new AssetsCore();
            cameraCore = new CameraCore();

            idService = new IDService();

        }

        public void Inject(UIApp uiApp) {
            this.uiApp = uiApp;
        }

        public RoleEntity Role_Owner() {
            roleRepository.TryGet(gameEntity.ownerRoleID, out var role);
            return role;
        }

    }

}