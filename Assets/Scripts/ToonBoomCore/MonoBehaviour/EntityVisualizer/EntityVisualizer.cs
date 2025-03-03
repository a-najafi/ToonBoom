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
        protected SpriteRenderer _spriteRenderer;

        protected IGridNodeEntity gridNodeEntity;

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