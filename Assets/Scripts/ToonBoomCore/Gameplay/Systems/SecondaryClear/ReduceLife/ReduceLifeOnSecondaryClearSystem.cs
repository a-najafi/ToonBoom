using ToonBoomCore.Gameplay.Entities.ReduceLife;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.SecondaryClear.ReduceLife
{
    public class ReduceLifeOnSecondaryClearSystem : SecondaryClearSystemBase
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public override void SecondaryClear(ILevelState levelState, IGridNodeEntity mainEntity, IGridNodeEntity secondaryEntity)
        {
         
            if (secondaryEntity is IReduceLifeOnSecondaryClear)
            {
                var reduceLifeOnSecondaryClear = secondaryEntity as IReduceLifeOnSecondaryClear;
                CoreSystemReferenceHandler.Instance.LifeSystem.DecreaseLife(levelState,reduceLifeOnSecondaryClear, reduceLifeOnSecondaryClear.LifeReduction);
            }
        }
        
        
    }
}