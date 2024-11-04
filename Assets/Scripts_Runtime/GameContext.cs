using System;

namespace NJM {

    public class GameContext {

        // ==== Unique Entity ====
        public GameEntity gameEntity;
        public ProgramEntity programEntity;

        // ==== Entity Repository ====
        public RoleRepository roleRepository;

        // ==== Cores ====
        public InputCore inputCore;
        public AssetsCore assetsCore;
        public CameraCore cameraCore;

        // ==== Services ====
        public IDService idService;

        public GameContext() {

            gameEntity = new GameEntity();
            programEntity = new ProgramEntity();

            roleRepository = new RoleRepository();

            inputCore = new InputCore();
            assetsCore = new AssetsCore();
            cameraCore = new CameraCore();

            idService = new IDService();

        }

        public RoleEntity Role_Owner() {
            roleRepository.TryGet(gameEntity.ownerRoleID, out var role);
            return role;
        }

    }

}