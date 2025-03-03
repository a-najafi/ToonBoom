using System.Threading.Tasks;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EventSequencer.Events
{
    public class EndGameEvent : GameEvent
    {
        [SerializeField]private bool isWin = false;
        
        public EndGameEvent(int timestamp, bool isWin) : base(timestamp)
        {
            this.isWin = isWin;
        }

        public override Task ExecuteAsync()
        {
            EndGameScreen.EndGameScreen endGameScreen = GameObject.FindObjectOfType<EndGameScreen.EndGameScreen>();
            endGameScreen?.Show(true, isWin ? "Win!" : "Lose!");
            Complete();
            return Task.CompletedTask;
        }
    }
}