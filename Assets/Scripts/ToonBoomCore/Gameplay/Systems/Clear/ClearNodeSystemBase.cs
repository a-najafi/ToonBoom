using System;
using System.Collections.Generic;
using ToonBoomCore.Gameplay.Systems.Clear.Block;
using ToonBoomCore.Gameplay.Systems.Clear.ReduceLifeOnClear;
using ToonBoomCore.Gameplay.Systems.Clear.ScoreObjectiveOnClear;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Clear
{
    public interface IClearNodeSystem : IGameSystem
    {
        void ClearNode(ILevelState levelState, int nodeIndex);
    }

    public abstract class ClearNodeSystemBase : GameSystem, IClearNodeSystem
    {
        public virtual void ClearNode(ILevelState levelState, int nodeIndex)
        {
            throw new NotImplementedException();
        }

    }
    
    
    public class ClearNodeSystem : GameSystem, IClearNodeSystem
    {
        ClearBlockSystem _clearBlockSystem = new ClearBlockSystem();
        ReduceLifeOnClearSystem _reduceLifeOnClearSystem = new ReduceLifeOnClearSystem();
        private ScoreObjectiveOnClearSystem _scoreObjectiveOnClear = new ScoreObjectiveOnClearSystem();
        public override void Initialize(ILevelState levelState)
        {
            _clearBlockSystem.Initialize(levelState);
            _reduceLifeOnClearSystem.Initialize(levelState);
            _scoreObjectiveOnClear.Initialize(levelState);
        }

        public ClearNodeSystem()
        {
            _clearBlockSystem = new ClearBlockSystem();
        }
        
        public virtual void ClearNode(ILevelState levelState, int nodeIndex)
        {
            IGridState gridState = levelState.GetGridState();
            List<IGridNodeEntity> entities = gridState.GetNodeAt(nodeIndex).GetEntities();
            for (int i = 0; i < entities.Count; i++)
            {
                _scoreObjectiveOnClear.ScoreEntityObjective(levelState, entities[i]);
                _clearBlockSystem.ClearBlock(levelState,entities[i]);
                _reduceLifeOnClearSystem.ReduceLifeOnClear(levelState, entities[i]);
                
            }
            
        }

    }
}