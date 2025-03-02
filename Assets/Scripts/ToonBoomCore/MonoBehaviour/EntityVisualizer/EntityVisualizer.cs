using ToonBoomCore.Grid;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EntityVisualizer
{

    public interface IEntityVisualizer
    {
        IGridNodeEntity GetBoundToEntity { get; }
        
        void Initialize(IGridNodeEntity gridNodeEntity);

        void Update();
        
        
    }
    public class EntityVisualizer : UnityEngine.MonoBehaviour, IEntityVisualizer
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private IGridNodeEntity gridNodeEntity;

        public IGridNodeEntity GetBoundToEntity => gridNodeEntity;
        
        public virtual void Initialize(IGridNodeEntity gridNodeEntity)
        {
            this.gridNodeEntity = gridNodeEntity;
        }

        public virtual void Update()
        {
            return;
        }
    }
}