using System;
using UnityEngine;

namespace NJM.Domains {

    public static class StageDomain {

        public static StageEntity Spawn(GameContext ctx, StageSignature stageSignature) {
            var entity = GameFactory.Stage_Create(ctx, stageSignature);
            ctx.stageRepository.Add(entity);
            return entity;
        }

    }

}