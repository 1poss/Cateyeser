using System;
using UnityEngine;
using NJM.Controllers;
using NJM.Template;

namespace NJM.MainEntry {

    public class ClientMain : MonoBehaviour {

        [SerializeField] Camera mainCamera;
        [SerializeField] Camera uiCamera;

        [SerializeField] GameSettingSO gameSettingSO;

        GameContext ctx;

        void Awake() {

            // ==== Ctor ====
            ctx = new GameContext();
            var uiApp = GetComponent<UIApp>();
            uiApp.Ctor();

            // ==== Inject ====
            ctx.Inject(uiApp);
            ctx.cameraCore.Inject(mainCamera, uiCamera);
            uiApp.Inject(ctx.assetsCore);
            ctx.assetsCore.Inject(gameSettingSO);

            // ==== Pre Init ====
            ctx.cameraCore.Init();

            // ==== Init ====
            ctx.inputCore.Init();

            Action action = async () => {

                await ctx.assetsCore.LoadAll();

                // ==== Enter ====
                GameController.Enter_FirstEnter(ctx, ctx.assetsCore.gameSettingSO.tm.startStage);

                var program = ctx.programEntity;
                program.isInit = true;

            };
            action.Invoke();

        }

        void Update() {

            var program = ctx.programEntity;
            if (!program.isInit) {
                return;
            }

            float dt = Time.deltaTime;

            // ==== Pre Tick ====
            var input = ctx.inputCore;
            input.Tick(dt);
            GameController.PreTick(ctx, dt);

            // ==== Fix Tick ====
            ref float fixRestSec = ref program.fixRestSec;
            fixRestSec += dt;
            if (fixRestSec < program.fixIntervalSec) {
                GameController.FixTick(ctx, fixRestSec);
                fixRestSec = 0;
            } else {
                while (fixRestSec >= program.fixIntervalSec) {
                    GameController.FixTick(ctx, program.fixIntervalSec);
                    fixRestSec -= program.fixIntervalSec;
                }
            }

            // ==== Late Tick ====
            GameController.LateTick(ctx, dt);

        }

        void OnDestroy() {
            TearDown();
        }

        void OnApplicationQuit() {
            TearDown();
        }

        void TearDown() {
            var program = ctx.programEntity;
            if (program.isTearDown) {
                return;
            }
            program.isTearDown = true;

            ctx.assetsCore.UnloadAll();
        }
    }
}
