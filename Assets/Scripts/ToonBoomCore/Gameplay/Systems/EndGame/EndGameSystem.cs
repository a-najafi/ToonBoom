using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EventSequencer.Events;
using ToonBoomCore.Player.State;
using ToonBoomCore.Player.Systems;

namespace ToonBoomCore.Gameplay.Systems.EndGame
{
    public class EndGameSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void CheckEndGame(ILevelState levelState)
        {
            
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            
            levelState.UseMove();
            
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            
            if(levelState.CheckVictoryCondition())
                WinGame(levelState);
            else if(levelState.CheckLoseCondition())
                LoseGame(levelState);
                
        }

        public void WinGame(ILevelState levelState)
        {
            PlayerState playerState = PlayerSystem.Instance.LoadPlayerState();
            PlayerSystem.Instance.CompleteLevel(playerState.CurrentLevel);
            
            CoreSystemReferenceHandler.Instance.EventSystem.QueuEvent(levelState, new EndGameEvent(levelState.GetTimeStamp(),true));
        }

        public void LoseGame(ILevelState levelState)
        {
            CoreSystemReferenceHandler.Instance.EventSystem.QueuEvent(levelState, new EndGameEvent(levelState.GetTimeStamp(),false));
        }
    }
}