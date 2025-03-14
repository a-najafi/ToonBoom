using System;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Entities.Moving;
using ToonBoomCore.Gameplay.Entities.PrefabVisualizer;
using ToonBoomCore.Gameplay.Entities.ReduceLife;
using ToonBoomCore.Gameplay.Entities.Score;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Entities.Strawberry
{
    [Serializable]
    public class Strawberry : GridNodeEntityBase, IReduceLifeOnSecondaryClear, IPrefabVisualizer, ICollisionEntity, IScoreEntity, IMovingEntity
    {
        
        [SerializeField]
        protected int currentLife = 1;
        
        [SerializeField]
        protected int lifeReduction = 1;
        
        [SerializeField]
        protected int score = 30;

        
        [SerializeField]
        protected SerializableAssetPath prefabVisualizerPath = new SerializableAssetPath();

        public Strawberry()
        {
            prefabVisualizerPath.SetPath("Visualizers/Strawberry");
            score = 30;
            lifeReduction = 1;
            currentLife = 1;
        }

        public Strawberry(Strawberry strawberry) : base(strawberry)
        {
            this.lifeReduction = strawberry.LifeReduction;
            this.currentLife = strawberry.currentLife;
            this.score = strawberry.score;
            this.prefabVisualizerPath.SetPath(strawberry.PrefabVisualizerPath);
        }


        public int GetCurrentLife => currentLife;
        public void ModifyLife(int amount)
        {
            currentLife += amount;
        }

        public override IGridNodeEntity GetCopy()
        {
            return new Strawberry(this);
        }

        public override void CopyTo(IGridNodeEntity other)
        {
            Strawberry otherBlockEntity = other as Strawberry;
            if (otherBlockEntity == null)
                throw new NullReferenceException();
            otherBlockEntity.lifeReduction = this.lifeReduction;
            otherBlockEntity.currentLife = currentLife;
            otherBlockEntity.score = score;
            otherBlockEntity.prefabVisualizerPath.SetPath(PrefabVisualizerPath);
        }

        public int GetScore()
        {
            return score;
        }

        public string PrefabVisualizerPath => prefabVisualizerPath.AssetPath;
        public int LifeReduction => lifeReduction;
    }
}