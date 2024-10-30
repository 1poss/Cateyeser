using System;
using UnityEngine;
using Cateyeser.Controllers;

namespace Cateyeser.MainEntry {

    public class ClientMain : MonoBehaviour {

        GameContext ctx;

        void Awake() {

            // ==== Ctor ====
            ctx = new GameContext();

            // ==== Inject ====

            // ==== Pre Init ====
            ctx.cameraCore.Init();

            // ==== Init ====
            ctx.inputCore.Init();

            Action action = async () => {

                await ctx.assetsCore.LoadAll();

                // ==== Enter ====
                GameController.Enter(ctx);

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
