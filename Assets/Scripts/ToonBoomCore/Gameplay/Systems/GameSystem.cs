using System;
using System.Collections.Generic;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems
{

    public interface IGameSystem
    {
        void Initialize(ILevelState levelState);
    }
    // this class can later be extended to handle its lifetime
    // other such systems would have different life times 
    // for example a PlayerSystem could be persistent between scenes but a game system can be discarded once the scene changes
    public abstract class GameSystem : IGameSystem
    {
        public virtual void Initialize(ILevelState levelState)
        {
            throw new System.NotImplementedException();
        }
        
      
    }
}