using System;
using ToonBoomCore.Grid;
using UnityEngine;

namespace ToonBoomCore.Level.Design
{
    public interface IEntitySpawnDesign
    {
        IGridNodeEntity EntityDesign { get; }
        
        // when an empty node is going to be filled the spawn design with highest priority is checked first
        // objective entities should have highest priority
        int Priority { get; } 
        
        // will skip this entity if maximum count of said entity in level is  currently met
        // will be skipped if value is -1
        int MaximumCountInLevel { get; } 
        
        //will skip if total number of entities spawned is larger than value
        int MaximumTotalSpawn { get;}
        
        //for each 
        float SpawnProbability { get; }
        
    }
    [Serializable]
    public abstract class EntitySpawnDesignBase : IEntitySpawnDesign
    {
        public virtual IGridNodeEntity EntityDesign { get; }
        public virtual int Priority { get; }
        public virtual int MaximumCountInLevel { get; }
        
        public virtual int MaximumTotalSpawn { get; }
        public virtual float SpawnProbability { get; }
    }

    [Serializable]
    public class EntitySpawnDesign : EntitySpawnDesignBase
    {
        [SerializeReference]
        private GridNodeEntityBase entityDesign;

        [SerializeField] private int priority= 1;
        [SerializeField] private int maximumCountInLevel = -1;
        [SerializeField] private int maximumTotalSpawn = -1;
        [SerializeField] private float spawnProbability = 1;
        
        
        public override IGridNodeEntity EntityDesign => entityDesign;
        public override int Priority => priority;
        public override int MaximumCountInLevel => maximumCountInLevel;
        
        public override int MaximumTotalSpawn => maximumTotalSpawn;
        
        public override float SpawnProbability => spawnProbability;
    }
}