using System;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Entities.Life;
using ToonBoomCore.Gameplay.Entities.Moving;
using ToonBoomCore.Gameplay.Entities.PrefabVisualizer;
using ToonBoomCore.Gameplay.Entities.Score;
using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace ToonBoomCore.Gameplay.Entities
{

    // blocks have collision and are affected by gravity they also have a life and can be cleared
    public interface IBlockEntity : IMovingEntity, ICollisionEntity,ILifeEntity, IScoreEntity, IPrefabVisualizer
    {
        public enum EBlockColor
        {
            Blue = 1,
            Red = 2,
            Yellow = 3,
            Green = 4
        }

        public EBlockColor BlockColor { get; }
    }
    
    
    [Serializable]
    public class BlockEntity: GridNodeEntityBase, IBlockEntity
    {
       
        
        [SerializeField]
        protected int currentLife = 1;
        
        [SerializeField]
        protected int score = 10;

        [SerializeField]
        protected IBlockEntity.EBlockColor blockColor = IBlockEntity.EBlockColor.Blue;
        
        [SerializeField]
        protected SerializableAssetPath prefabVisualizerPath = new SerializableAssetPath();

        public BlockEntity()
        {
            
        }

        public BlockEntity(BlockEntity blockEntity) : base(blockEntity)
        {
            this.blockColor = blockEntity.BlockColor;
            this.currentLife = blockEntity.currentLife;
            this.score = blockEntity.score;
            this.prefabVisualizerPath.SetPath(blockEntity.PrefabVisualizerPath);
        }

        public IBlockEntity.EBlockColor BlockColor => this.blockColor;

        public int GetCurrentLife => currentLife;
        public void ModifyLife(int amount)
        {
            currentLife += amount;
        }

        public override IGridNodeEntity GetCopy()
        {
            return new BlockEntity(this);
        }

        public override void CopyTo(IGridNodeEntity other)
        {
            BlockEntity otherBlockEntity = other as BlockEntity;
            if (otherBlockEntity == null)
                throw new NullReferenceException();
            
            otherBlockEntity.currentLife = currentLife;
            otherBlockEntity.blockColor = blockColor;
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