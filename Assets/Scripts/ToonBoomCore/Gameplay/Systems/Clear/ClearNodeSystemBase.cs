using System;
using System.Collections.Generic;
using ToonBoomCore.Gameplay.Systems.Clear.Block;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Clear
{
    public interface IClearNodeSystem : IGameSystem
    {
        void ClearNode(IGridState gridState, int nodeIndex);
    }

    public abstract class ClearNodeSystemBase : GameSystem, IClearNodeSystem
    {
        public virtual void ClearNode(IGridState gridState, int nodeIndex)
        {
            throw new NotImplementedException();
        }

    }
    
    
    public class ClearNodeSystem : GameSystem, IClearNodeSystem
    {
        ClearBlockSystem _clearBlockSystem = new ClearBlockSystem();

        public override void Initialize(ILevelState levelState)
        {
            _clearBlockSystem.Initialize(levelState);
        }

        public ClearNodeSystem()
        {
            _clearBlockSystem = new ClearBlockSystem();
        }
        
        public virtual void ClearNode(IGridState gridState, int nodeIndex)
        {
            List<IGridNodeEntity> entities = gridState.GetNodeAt(nodeIndex).GetEntities();
            for (int i = 0; i < entities.Count; i++)
            {
                _clearBlockSystem.ClearBlock(gridState,entities[i]);
            }
            
        }

    }
}