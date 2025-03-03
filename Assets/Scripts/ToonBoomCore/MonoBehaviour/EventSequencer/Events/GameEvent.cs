namespace ToonBoomCore.MonoBehaviour.EventSequencer.Events
{
    using System;
    using System.Threading.Tasks;

    public abstract class GameEvent
    {
        public int Timestamp { get; }
        public event Action OnComplete; // Triggered when event completes

        protected GameEvent(int timestamp)
        {
            Timestamp = timestamp;
        }

        public abstract Task ExecuteAsync(); // Runs the event

        protected void Complete()
        {
            OnComplete?.Invoke(); // Notify when event completes
        }
    }

}