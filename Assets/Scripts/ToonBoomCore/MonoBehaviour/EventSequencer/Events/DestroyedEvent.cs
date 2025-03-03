using System.Threading.Tasks;
using DG.Tweening;
using ToonBoomCore.GameManager;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EventSequencer.Events
{
    public class DestroyedEvent : GameEvent
    {
        private IGridNodeEntity entity;
        private int nodeIndex;
        
        public DestroyedEvent(int timestamp, IGridNodeEntity entity, int nodeIndex) : base(timestamp)
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
            
            entityVisualizerTransform.localScale = Vector3.one;

            entityVisualizer.SetActive(true);



            Sequence sequence = DOTween.Sequence();

            sequence.Append(entityVisualizerTransform.DOScale(1.2f, 0.2f));
            sequence.Append(entityVisualizerTransform.DOScale(.2f, 0.3f));
            sequence.SetAutoKill(true);
            
            await sequence.AsyncWaitForCompletion();
            
            if(ToonBoomGameManager.Instance != null && entity != null)
                ToonBoomGameManager.Instance.EntityVisualizer.RemoveVisualizerForEntity(entity, nodeIndex);

            Complete();


        }
    }
}