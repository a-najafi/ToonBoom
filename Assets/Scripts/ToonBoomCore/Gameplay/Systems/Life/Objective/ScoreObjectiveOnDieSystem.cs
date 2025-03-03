using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Life;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Entities.Score;
using ToonBoomCore.Gameplay.Entities.Strawberry;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design.Objectives;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Life.Objective
{
    public class ScoreObjectiveOnDieSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void ScoreEntityObjective(ILevelState levelState, ILifeEntity lifeEntity)
        {
            if (lifeEntity is IScoreEntity scoreEntity)
            {
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,ScoreObjective.Name, scoreEntity.GetScore());    
            }
            if (lifeEntity is BlockEntity blockEntity)
            {
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,ClearBlockCountObjective.Name);
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,ClearBlockColorCountObjective.MakeCombinedName(blockEntity.BlockColor));
            }
            else if(lifeEntity is PowerupEntity powerupEntity)
            {
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,ClearPowerupCountObjective.Name);
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,ClearPowerupTypeCountObjective.MakeCombinedName(powerupEntity.GetType()));
            }
            else if (lifeEntity is Strawberry)
            {
                CoreSystemReferenceHandler.Instance.ScoreObjectiveSystem.ScoreObjective(levelState,ClearStrawberryObjective.Name);
            }
        }
    }
}