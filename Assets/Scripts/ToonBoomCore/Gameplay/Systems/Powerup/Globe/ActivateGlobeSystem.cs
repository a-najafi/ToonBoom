using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Powerup.Globe
{
    public class ActivateGlobeSystem : ActivatePowerupSystemBase
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public override void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            if (powerUpEntity is Entities.Powerup.Globe.Globe globe)
            {
                ActivateGlobe(levelState,globe.GetIndex(),globe.BlockColor);
            }
            
        }

        public void ActivateGlobe(ILevelState levelState, int nodeIndex, IBlockEntity.EBlockColor color)
        {

            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState,nodeIndex);
            List<int> nodesToClear = GetAllNodesWithBlocksOfColor(levelState, color);
            for (int i = 0; i < nodesToClear.Count; i++)
            {
                CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState,
                    nodesToClear[i]);
            }
        }

        public List<int> GetAllNodesWithBlocksOfColor(ILevelState levelState, IBlockEntity.EBlockColor color)
        {
            List<int> nodesOfColor = new List<int>();
            
            IGridState gridState = levelState.GetGridState();

            List<IGridNode> Nodes = gridState.GetNodes();

            for (int i = 0; i < Nodes.Count; i++)
            {
                List<IGridNodeEntity> entities = Nodes[i].GetEntities();
                for (int j = 0; j < entities.Count(); j++)
                {
                    if (entities[j] is BlockEntity blockEntity && blockEntity.BlockColor == color)
                    {
                        nodesOfColor.Add(i);
                        break;
                    }
                }
            }

            return nodesOfColor;


        }
    }
}