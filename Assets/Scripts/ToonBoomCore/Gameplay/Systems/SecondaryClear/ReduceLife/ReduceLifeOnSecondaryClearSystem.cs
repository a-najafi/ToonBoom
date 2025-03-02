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

        public override void SecondaryClear(IGridState gridState, int mainNodeIndex, int secondaryNodeIndex)
        {
            gridState.GetNodeAt(secondaryNodeIndex).GetEntities().ForEach(entity =>
            {
                if (entity is IReduceLifeOnSecondaryClear)
                {
                    var reduceLifeOnSecondaryClear = entity as IReduceLifeOnSecondaryClear;
                    CoreSystemReferenceHandler.Instance.LifeSystem.DecreaseLife(reduceLifeOnSecondaryClear, reduceLifeOnSecondaryClear.LifeReduction);
                }
            });  
            base.SecondaryClear(gridState, mainNodeIndex, secondaryNodeIndex);
        }
        
        
    }
}