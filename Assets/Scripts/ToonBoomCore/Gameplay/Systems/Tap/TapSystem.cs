using System;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Gameplay.Systems.Powerup;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EventSequencer.Events;


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
        private TapPowerupSystem _tapPowerupSystem = new TapPowerupSystem();
        
        
        public override void Initialize(ILevelState levelState)
        {
            _blockSystem.Initialize(levelState);
            _tapPowerupSystem.Initialize(levelState);


        }

        public override void TapNode(ILevelState levelState, int nodeIndex)
        {
            if(_tapPowerupSystem.CheckForPowerup(levelState.GetGridState(),nodeIndex))
                _tapPowerupSystem.TapNode(levelState, nodeIndex);
            else if(_blockSystem.CheckForBlock(levelState.GetGridState(), nodeIndex))
                _blockSystem.TapNode(levelState,nodeIndex);
            
            CoreSystemReferenceHandler.Instance.EventSystem.QueuEvent(levelState,new ObjectiveScoredEvent(levelState.GetTimeStamp(),levelState));
            
            CoreSystemReferenceHandler.Instance.EndGameSystem.CheckEndGame(levelState);
        }
    }
    
}