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

        public virtual void ClearBlock(ILevelState levelState, IGridNodeEntity gridNodeEntity)
        {
            if (gridNodeEntity is IBlockEntity)
            {
                List<int> adjacentNodes = levelState.GetGridState().GetAdjacentNodes(gridNodeEntity.GetIndex());
                
                //blocks do a secondary clear on adjacent nodes
                //(example usage is entities that give resources when blocks adjacent are cleared or baloons that are cleared themselves when adjacent blocks are cleared)
                for (int i = 0; i < adjacentNodes.Count; i++)
                {
                    CoreSystemReferenceHandler.Instance.SecondaryClearSystem.SecondaryClear(levelState, gridNodeEntity,adjacentNodes[i]);
                }
                
            }
        }
    }
}