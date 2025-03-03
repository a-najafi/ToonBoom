using System;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EventSequencer.Events;

namespace ToonBoomCore.MonoBehaviour.EventSequencer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UnityEngine;

    public class EventSequencer : MonoBehaviour
    {
        private SortedDictionary<int, List<GameEvent>>
            eventQueue = new SortedDictionary<int, List<GameEvent>>(); // Stores events grouped by timestamp
        
        private bool isProcessing = false;
        
        public bool IsProcessing => isProcessing;

        

        public void Initialize(ILevelState levelState)
        {
            CoreSystemReferenceHandler.Instance.EventSystem.OnGameEventQueued.AddListener(QueueEvent);
        }


        public void QueueEvent(GameEvent gameEvent)
        {
            if (!eventQueue.ContainsKey(gameEvent.Timestamp))
            {
                eventQueue[gameEvent.Timestamp] = new List<GameEvent>();
            }

            eventQueue[gameEvent.Timestamp].Add(gameEvent);

            // Only start processing if it's not already running
            if (!isProcessing)
            {
                isProcessing = true;
                StartCoroutine(DelayedStartProcessing()); // Delay StartProcessing to next frame
            }
        }

        private IEnumerator<YieldInstruction> DelayedStartProcessing()
        {
            yield return null; // Ensures all same-timestamp events are queued before execution
            StartProcessing();
        }

        private async void StartProcessing()
        {
            while (eventQueue.Count > 0)
            {
                int timestamp = eventQueue.Keys.First();
                List<GameEvent> eventsToProcess = eventQueue[timestamp];

                eventQueue.Remove(timestamp); // Remove batch before execution
                
                // Run all same-timestamp events in parallel
                List<Task> runningTasks = new List<Task>();
                foreach (var gameEvent in eventsToProcess)
                {
                    runningTasks.Add(gameEvent.ExecuteAsync());
                }

                await Task.WhenAll(runningTasks); // Wait for all events at this timestamp to finish
            }

            isProcessing = false;
        }

        
    }
}

