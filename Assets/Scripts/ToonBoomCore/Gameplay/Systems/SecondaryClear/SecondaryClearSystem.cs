using System.Collections.Generic;
using ToonBoomCore.Gameplay.Systems.Clear.ReduceLifeOnClear;
using ToonBoomCore.Gameplay.Systems.SecondaryClear.ReduceLife;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.SecondaryClear
{

    public interface ISecondaryClearSystem
    {
        void SecondaryClear(ILevelState levelState, IGridNodeEntity mainEntity, IGridNodeEntity secondaryEntity);
    }
    
    public abstract class SecondaryClearSystemBase : GameSystem, ISecondaryClearSystem
    {
        public virtual void SecondaryClear(ILevelState levelState, IGridNodeEntity mainEntity, IGridNodeEntity secondaryEntity)
        {
            throw new System.NotImplementedException();
        }
        
    }


    public class SecondaryClearSystem : SecondaryClearSystemBase
    {
        ReduceLifeOnSecondaryClearSystem _reduceLifeOnClearSystem = new ReduceLifeOnSecondaryClearSystem();
        public override void Initialize(ILevelState levelState)
        {
            _reduceLifeOnClearSystem.Initialize(levelState);
        }

        public void SecondaryClear(ILevelState levelState, IGridNodeEntity mainEntity, int secondaryNodeIndex)
        {
            IGridState gridState = levelState.GetGridState();
            List<IGridNodeEntity> entities = gridState.GetNodeAt(secondaryNodeIndex).GetEntities();
            for (int i = 0; i < entities.Count; i++)
            {
                SecondaryClear(levelState,  mainEntity,entities[i]);
            }
            
        }
        
        public override void SecondaryClear(ILevelState levelState, IGridNodeEntity mainEntity, IGridNodeEntity secondaryEntity)
        {
            _reduceLifeOnClearSystem.SecondaryClear(levelState,mainEntity, secondaryEntity);
        }
    }
}