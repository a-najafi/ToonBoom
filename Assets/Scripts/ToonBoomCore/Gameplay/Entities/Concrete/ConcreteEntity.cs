using System;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Entities.PrefabVisualizer;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Entities.Concrete
{
    
    // has collision preventing upper entities on going down but does not move itself
    public class ConcreteEntity : GridNodeEntityBase, ICollisionEntity, IPrefabVisualizer
    {
        
        [SerializeField]
        protected SerializableAssetPath prefabVisualizerPath = new SerializableAssetPath();




        public ConcreteEntity(ConcreteEntity concreteEntity) : base(concreteEntity)
        {
            prefabVisualizerPath.SetPath( concreteEntity.PrefabVisualizerPath);
        }
        public ConcreteEntity()
        {
            prefabVisualizerPath.SetPath("Visualizers/Concrete");
        }

        public string PrefabVisualizerPath => prefabVisualizerPath?.AssetPath;

        public override IGridNodeEntity GetCopy()
        {
            return new ConcreteEntity(this);
        }
        
        public override void CopyTo(IGridNodeEntity other)
        {
            ConcreteEntity otherConcreteEntity = other as ConcreteEntity;
            if (otherConcreteEntity == null)
                throw new NullReferenceException();
            otherConcreteEntity.prefabVisualizerPath.SetPath(PrefabVisualizerPath);
            
        }
    }
}