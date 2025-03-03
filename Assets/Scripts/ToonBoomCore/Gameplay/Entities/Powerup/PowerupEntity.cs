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
        int PowerPriority { get; }
        
        int Range { get; }
        
        public IBlockEntity.EBlockColor BlockColor { get; set; }
        
        
        
    }
    
    [Serializable]
    public abstract class PowerupEntity : GridNodeEntityBase , IPowerUpEntity
    {
        [SerializeField]
        protected int powerPriority = 1;
        
        [SerializeField]
        protected int currentLife = 1;
        
        [SerializeField]
        protected int score = 10;
        
        [SerializeField]
        protected int range = -1;
        
        
        [SerializeField]private IBlockEntity.EBlockColor blockColor = IBlockEntity.EBlockColor.Blue;

        public IBlockEntity.EBlockColor BlockColor
        {
            get { return blockColor; }
            set { blockColor = value; }
        }

        [SerializeField]
        protected SerializableAssetPath prefabVisualizerPath = new SerializableAssetPath();
        
        public PowerupEntity()
        {
            
        }

        public PowerupEntity(PowerupEntity powerupEntity) : base(powerupEntity)
        {
            this.blockColor = powerupEntity.blockColor;
            this.currentLife = powerupEntity.currentLife;
            this.score = powerupEntity.score;
            this.powerPriority = powerupEntity.powerPriority;
            this.range = powerupEntity.range;
            this.prefabVisualizerPath.SetPath(powerupEntity.PrefabVisualizerPath);
        }

        public int GetCurrentLife => currentLife;
        
        public void ModifyLife(int amount)
        {
            currentLife += amount;
        }

        public override IGridNodeEntity GetCopy()
        {
            throw new NotImplementedException();
        }

        public override void CopyTo(IGridNodeEntity other)
        {
            PowerupEntity otherPowerupEntity = other as PowerupEntity;
            if (otherPowerupEntity == null)
                throw new NullReferenceException();
            otherPowerupEntity.blockColor = BlockColor;
            otherPowerupEntity.prefabVisualizerPath.SetPath(PrefabVisualizerPath);
            otherPowerupEntity.currentLife = currentLife;
            otherPowerupEntity.score = score;
            otherPowerupEntity.powerPriority = powerPriority;
            otherPowerupEntity.range = range;
        }

        public int GetScore()
        {
            return score;
        }

        public string PrefabVisualizerPath => prefabVisualizerPath.AssetPath;

        public int PowerPriority => powerPriority;
        public int Range => range;
    }
}