using System.Collections.Generic;
using ToonBoomCore.Gameplay.Systems.Clear.ReduceLifeOnClear;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.SecondaryClear
{

    public interface ISecondaryClearSystem
    {
        void SecondaryClear(IGridState gridState, int mainNodeIndex, int secondaryNodeIndex);
    }
    
    public abstract class SecondaryClearSystemBase : GameSystem, ISecondaryClearSystem
    {
        public virtual void SecondaryClear(IGridState gridState, int mainNodeIndex, int secondaryNodeIndex)
        {
            throw new System.NotImplementedException();
        }
        
    }


    public class SecondaryClearSystem : SecondaryClearSystemBase
    {
        ReduceLifeOnClearSystem _reduceLifeOnClearSystem = new ReduceLifeOnClearSystem();
        public override void Initialize(ILevelState levelState)
        {
            _reduceLifeOnClearSystem.Initialize(levelState);
        }

        public override void SecondaryClear(IGridState gridState, int mainNodeIndex, int secondaryNodeIndex)
        {
            List<IGridNodeEntity> entities = gridState.GetNodeAt(secondaryNodeIndex).GetEntities();
            for (int i = 0; i < entities.Count; i++)
            {
                _reduceLifeOnClearSystem.ReduceLifeOnClear(gridState,entities[i]);    
            }
            
        }
    }
}