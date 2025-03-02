using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Clear.Block
{
    public class ClearBlockSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
        
        }

        public virtual void ClearBlock(IGridState gridState, IGridNodeEntity gridNodeEntity)
        {
            if (gridNodeEntity is IBlockEntity)
            {
                List<int> adjacentNodes = gridState.GetAdjacentNodes(gridNodeEntity.GetIndex());
                
                CoreSystemReferenceHandler.Instance.LifeSystem.DecreaseLife((gridNodeEntity as IBlockEntity),1);
                
                for (int i = 0; i < adjacentNodes.Count; i++)
                {
                    CoreSystemReferenceHandler.Instance.SecondaryClearSystem.SecondaryClear(gridState, gridNodeEntity.GetIndex(),adjacentNodes[i]);
                }
                CoreSystemReferenceHandler.Instance.EntityOnGridSystem.RemoveEntityFromGridAt(gridState,gridNodeEntity, gridNodeEntity.GetIndex());
                
                CoreSystemReferenceHandler.Instance.EntityPoolSystem.ReturnToPool(gridNodeEntity);
            }
        }
    }
}