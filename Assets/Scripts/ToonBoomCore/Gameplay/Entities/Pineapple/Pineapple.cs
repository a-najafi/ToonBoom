using System;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Entities.PrefabVisualizer;
using ToonBoomCore.Gameplay.Entities.Score;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Entities.Pineapple
{
    [Serializable]
    public class Pineapple : GridNodeEntityBase, IPrefabVisualizer, ICollisionEntity, IScoreEntity
    {
        
        
        [SerializeField]
        protected int score = 30;

        
        [SerializeField]
        protected SerializableAssetPath prefabVisualizerPath = new SerializableAssetPath();

        public Pineapple()
        {
            prefabVisualizerPath.SetPath("Visualizers/Pineapple");
            score = 30;
        }

        public Pineapple(Pineapple strawberry) : base(strawberry)
        {
            this.score = strawberry.score;
            this.prefabVisualizerPath.SetPath(strawberry.PrefabVisualizerPath);
        }



        public override IGridNodeEntity GetCopy()
        {
            return new Pineapple(this);
        }

        public override void CopyTo(IGridNodeEntity other)
        {
            Pineapple otherBlockEntity = other as Pineapple;
            if (otherBlockEntity == null)
                throw new NullReferenceException();
            otherBlockEntity.score = score;
            otherBlockEntity.prefabVisualizerPath.SetPath(PrefabVisualizerPath);
        }

        public int GetScore()
        {
            return score;
        }

        public string PrefabVisualizerPath => prefabVisualizerPath.AssetPath;
    }
}