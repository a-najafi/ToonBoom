using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Life;
using ToonBoomCore.Gameplay.Entities.Pineapple;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Entities.Score;
using ToonBoomCore.Gameplay.Entities.Strawberry;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design.Objectives;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Clear.ScoreObjectiveOnClear
{
    public class ScoreObjectiveOnClearSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void ScoreEntityObjective(ILevelState levelState, IGridNodeEntity gridNodeEntity)
        {

            if (gridNodeEntity is Pineapple)
            {
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,GainPineappleObjective.Name);
            }
            
            //score entities that are not destroyed are scored when cleared
            if (gridNodeEntity is IScoreEntity scoreEntity && !(gridNodeEntity is ILifeEntity))
            {
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,ScoreObjective.Name,scoreEntity.GetScore());
            }
            

        }
    }
}