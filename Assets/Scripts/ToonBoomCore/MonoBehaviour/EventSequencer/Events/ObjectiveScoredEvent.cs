using System.Threading.Tasks;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.UI.ObjectiveDisplay;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EventSequencer.Events
{
    public class ObjectiveScoredEvent : GameEvent
    {
        private ILevelState levelState;
        public ObjectiveScoredEvent(int timestamp, ILevelState levelState) : base(timestamp)
        {
            this.levelState = levelState;
        }

        public override Task ExecuteAsync()
        {
            GameObject.FindObjectOfType<ObjectiveDisplay>()?.UpdateObjectiveDisplay(levelState);
            Complete();
            return Task.CompletedTask;
        }
    }
}