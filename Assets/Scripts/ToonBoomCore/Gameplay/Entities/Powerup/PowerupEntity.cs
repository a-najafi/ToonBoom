using System;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Entities.Life;
using ToonBoomCore.Gameplay.Entities.Moving;
using ToonBoomCore.Gameplay.Entities.PrefabVisualizer;
using ToonBoomCore.Gameplay.Entities.Score;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Entities.Powerup
{
    // powerups have collision and are affected by gravity they also have a life and can be cleared
    public interface IPowerUpEntity : IMovingEntity, ICollisionEntity, ILifeEntity, IScoreEntity, IPrefabVisualizer
    {
        
    }
    
    [Serializable]
    public class PowerupEntity : GridNodeEntityBase , IPowerUpEntity
    {
        [SerializeField]
        private int currentLife = 1;
        
        [SerializeField]
        private int score = 10;
        
        [SerializeField]
        private SerializableAssetPath prefabVisualizerPath = new SerializableAssetPath();
        
        public PowerupEntity()
        {
            
        }
        public PowerupEntity(int index, int currentLife = 1) : base(index)
        {
            this.currentLife = currentLife;
        }

        public int GetCurrentLife => currentLife;
        
        public void ModifyLife(int amount)
        {
            currentLife += amount;
        }

        public override IGridNodeEntity GetCopy()
        {
            return new PowerupEntity(currentLife);
        }

        public override void CopyTo(IGridNodeEntity other)
        {
            PowerupEntity otherBlockEntity = other as PowerupEntity;
            if (otherBlockEntity == null)
                throw new NullReferenceException();
            
            otherBlockEntity.currentLife = currentLife;

        }

        public int GetScore()
        {
            return score;
        }

        public string PrefabVisualizerPath => prefabVisualizerPath.AssetPath;
    }
}