using ToonBoomCore.Gameplay.Entities.Life;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Gameplay.Systems.Life.Objective;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Life
{
    public class LifeSystem : GameSystem
    {
        private ScoreObjectiveOnDieSystem _scoreObjectiveOnDieSystem = new ScoreObjectiveOnDieSystem();
        
        public override void Initialize(ILevelState levelState)
        {
            _scoreObjectiveOnDieSystem.Initialize(levelState);
        }

        public virtual void IncreaseLife(ILevelState levelState,ILifeEntity lifeEntity, int amount)
        {
            lifeEntity.ModifyLife(+amount);   
        }
        
        public virtual void DecreaseLife(ILevelState levelState,ILifeEntity lifeEntity, int amount)
        {
            lifeEntity.ModifyLife(-amount);
            if (lifeEntity.GetCurrentLife <= 0)
            {
                
                _scoreObjectiveOnDieSystem.ScoreEntityObjective(levelState,lifeEntity);
                
                CoreSystemReferenceHandler.Instance.EntityOnGridSystem.RemoveEntityFromGridAt(levelState, lifeEntity,lifeEntity.GetIndex());
                CoreSystemReferenceHandler.Instance.EntityPoolSystem.ReturnToPool(lifeEntity);   
                
               
            }    
        }
    }
}