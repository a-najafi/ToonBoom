using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EventSequencer.Events;
using UnityEngine.Events;

namespace ToonBoomCore.Gameplay.Systems.Event
{
    public class EventSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            OnGameEventQueued.RemoveAllListeners();
        }
        
        public UnityEvent<GameEvent> OnGameEventQueued = new UnityEvent<GameEvent>();

        public void QueuEvent(ILevelState levelState, GameEvent gameEvent)
        {
            levelState.QueueGameEvent(gameEvent);
            OnGameEventQueued.Invoke(gameEvent);
        }

        public void IncrementTimeStamp(ILevelState levelState)
        {
            levelState.IncrementTimeStamp();
        }
    }
}