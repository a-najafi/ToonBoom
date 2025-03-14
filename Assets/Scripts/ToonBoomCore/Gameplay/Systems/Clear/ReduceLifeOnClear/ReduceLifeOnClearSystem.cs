using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Life;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Clear.ReduceLifeOnClear
{
    public class ReduceLifeOnClearSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void ReduceLifeOnClear(ILevelState levelState, IGridNodeEntity gridNodeEntity)
        {
            if (gridNodeEntity is ILifeEntity lifeEntity)
            {
                List<int> adjacentNodes = levelState.GetGridState().GetAdjacentNodes(gridNodeEntity.GetIndex());
                
                CoreSystemReferenceHandler.Instance.LifeSystem.DecreaseLife(levelState,lifeEntity,1);

                
            }
            
        }
    }
}