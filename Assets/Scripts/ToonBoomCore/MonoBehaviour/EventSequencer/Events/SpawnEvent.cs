using System.Threading.Tasks;
using DG.Tweening;
using ToonBoomCore.GameManager;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EventSequencer.Events
{
    public class SpawnEvent : GameEvent
    {
        private IGridNodeEntity entity;
        private int nodeIndex;
        
        public SpawnEvent(int timestamp, IGridNodeEntity entity, int nodeIndex) : base(timestamp)
        {
            this.nodeIndex = nodeIndex;
            this.entity = entity;
        }

        public override async Task ExecuteAsync()
        {

            GameObject entityVisualizer = ToonBoomGameManager.Instance.EntityVisualizer.GetVisualizerForEntity(entity);
            Transform entityVisualizerTransform = entityVisualizer.transform;

            entityVisualizerTransform.DOKill();
            
            entityVisualizerTransform.transform.position =
                ToonBoomGameManager.Instance.EntityVisualizer.GetWorldPosition(nodeIndex);
            
            entityVisualizerTransform.localScale = Vector3.zero;

            entityVisualizer.SetActive(true);



            Sequence sequence = DOTween.Sequence();

            sequence.Append(entityVisualizerTransform.DOScale(1.2f, 0.2f));
            sequence.Append(entityVisualizerTransform.DOScale(1f, 0.1f));
            sequence.SetAutoKill(true);
            await sequence.AsyncWaitForCompletion();

            Complete();


        }
    }
}