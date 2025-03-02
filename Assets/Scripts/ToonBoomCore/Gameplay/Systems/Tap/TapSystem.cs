using System;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;


namespace ToonBoomCore.Gameplay.Systems
{

    public interface ITapSystem : IGameSystem
    {
        void TapNode(ILevelState levelState, int nodeIndex);
    }

    public abstract class TapSystemBase : GameSystem, ITapSystem
    {
        public virtual void TapNode(ILevelState levelState, int nodeIndex)
        {
            throw new NotImplementedException();
        }

    }

    public class TapSystem : TapSystemBase
    {
        private BlockTapSystem _blockSystem = new BlockTapSystem();
        
        
        public override void Initialize(ILevelState levelState)
        {
            _blockSystem.Initialize(levelState);



        }

        public override void TapNode(ILevelState levelState, int nodeIndex)
        {
            _blockSystem.TapNode(levelState,nodeIndex);
            
        }
    }
    
}