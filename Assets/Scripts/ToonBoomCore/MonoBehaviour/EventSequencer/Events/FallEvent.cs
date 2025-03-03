using System.Threading.Tasks;
using DG.Tweening;
using ToonBoomCore.GameManager;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EventSequencer.Events
{
    public class FallEvent : GameEvent
    {
        private IGridNodeEntity entity;
        private int fromNodeIndex;
        private int toNodeIndex;
        
        public FallEvent(int timestamp, IGridNodeEntity entity, int fromNodeIndex, int toNodeIndex) : base(timestamp)
        {
            this.entity = entity;
            this.fromNodeIndex = fromNodeIndex;
            this.toNodeIndex = toNodeIndex;
        }

        public override async Task ExecuteAsync()
        {

            GameObject entityVisualizer = ToonBoomGameManager.Instance.EntityVisualizer.GetVisualizerForEntity(entity);
            Transform entityVisualizerTransform = entityVisualizer.transform;
            entityVisualizerTransform.DOKill();
            entityVisualizerTransform.transform.position =
                ToonBoomGameManager.Instance.EntityVisualizer.GetWorldPosition(fromNodeIndex);


            Sequence sequence = DOTween.Sequence();
            
            
            Sequence scaleSequence = DOTween.Sequence();
            scaleSequence.Join(entityVisualizerTransform.DOScaleY(1.5f, 0.1f));
            scaleSequence.Join(entityVisualizerTransform.DOScaleX(.5f, 0.1f));

            sequence.Append(scaleSequence);
            
            Vector3 fromPosition = ToonBoomGameManager.Instance.EntityVisualizer.GetWorldPosition(fromNodeIndex);
            Vector3 toPosition = ToonBoomGameManager.Instance.EntityVisualizer.GetWorldPosition(toNodeIndex);
            float duration = 0.1f * (fromPosition.y - toPosition.y);
            
            sequence.Append(entityVisualizerTransform.DOMoveY( toPosition.y, duration));
            
            scaleSequence = DOTween.Sequence();
            scaleSequence.Join(entityVisualizerTransform.DOScaleY(.5f, 0.1f));
            scaleSequence.Join(entityVisualizerTransform.DOScaleX(1.5f, 0.1f));
            
            sequence.Append(scaleSequence);
            sequence.Append(entityVisualizerTransform.DOScale(1f, 0.1f));
            
            sequence.SetAutoKill(true);
            await Task.Delay(200);

            Complete();


        }
    }
}