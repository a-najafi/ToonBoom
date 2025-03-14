using System;
using System.Collections.Generic;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design;
using ToonBoomCore.Level.State;
using UnityEngine;
using UnityEngine.Events;

namespace ToonBoomCore.Gameplay.Systems.EntityPool
{
    public class EntityPoolSystem : GameSystem
    {
        
        
        private Dictionary<Type, int> totalSpawnedEntitiesCount = new Dictionary<Type, int>();
        private Dictionary<Type, Queue<IGridNodeEntity>> availableEntities = new Dictionary<Type, Queue<IGridNodeEntity>>();
        private Dictionary<Type, HashSet<IGridNodeEntity>> inUseEntities = new Dictionary<Type, HashSet<IGridNodeEntity>>();

        public override void Initialize(ILevelState levelState)
        {
            inUseEntities.Clear();
            availableEntities.Clear();
            totalSpawnedEntitiesCount.Clear();
        }

        protected void ResolveContainers<T>()
        {
            var type = typeof(T);
            if(!availableEntities.ContainsKey(type))
                availableEntities.Add(type, new Queue<IGridNodeEntity>());
            
            if(!inUseEntities.ContainsKey(type))
                inUseEntities.Add(type, new HashSet<IGridNodeEntity>());
            

        }
        
        protected void ResolveContainers(Type type)
        {
            if(!availableEntities.ContainsKey(type))
                availableEntities.Add(type, new Queue<IGridNodeEntity>());
            
            if(!inUseEntities.ContainsKey(type))
                inUseEntities.Add(type, new HashSet<IGridNodeEntity>());
            
            if(!totalSpawnedEntitiesCount.ContainsKey(type))
                totalSpawnedEntitiesCount.Add(type,0);
        }


        public IGridNodeEntity GetNewInstanceOf(IGridNodeEntity gridNodeEntityDesign) 
        {
            Type type = gridNodeEntityDesign.GetType();
            //Debug.Log("requested type is " + type.FullName);
            
            
            ResolveContainers(type);
            
            totalSpawnedEntitiesCount[type]++;
            
            Queue<IGridNodeEntity> queue = availableEntities[type];

            IGridNodeEntity gridNodeEntity;
            
            if (queue.Count == 0)
            {
                gridNodeEntity = gridNodeEntityDesign.GetCopy();
                
                inUseEntities[type].Add(gridNodeEntity);
                
                return gridNodeEntity;
            }

            gridNodeEntity = queue.Dequeue();
            gridNodeEntityDesign.CopyTo(gridNodeEntity);
            
            inUseEntities[type].Add(gridNodeEntity);
            
            return gridNodeEntity;
        }

        public void ReturnToPool(IGridNodeEntity gridNodeEntity)
        {
            Type gridNodeEntityType = gridNodeEntity.GetType();
            
            ResolveContainers(gridNodeEntityType);
            
            inUseEntities[gridNodeEntityType].Remove(gridNodeEntity);
            availableEntities[gridNodeEntityType].Enqueue(gridNodeEntity);
            
        }
        
        // these state checks should not be using system parameters 
        // will have to move these data to a state object 

        public int NumberOfActiveEntities(IGridNodeEntity gridNodeEntity)
        {
            if(inUseEntities.ContainsKey(gridNodeEntity.GetType()))
                return inUseEntities[gridNodeEntity.GetType()].Count;
            return 0;
        }

        public int NumberOfTotalSpawnedEntities(IGridNodeEntity gridNodeEntity)
        {
            if(totalSpawnedEntitiesCount.ContainsKey(gridNodeEntity.GetType()))
                return totalSpawnedEntitiesCount[gridNodeEntity.GetType()];
            return 0;
        }
        
        public bool CheckEntitySpawnDesignRequirements(IEntitySpawnDesign entitySpawnDesign)
        {
            
            if (entitySpawnDesign == null)
                return false;
            Type entityType = entitySpawnDesign.GetType();
            ResolveContainers(entityType);

            if (NumberOfTotalSpawnedEntities(entitySpawnDesign.EntityDesign) >= entitySpawnDesign.MaximumTotalSpawn)
                return false;

            if (NumberOfActiveEntities(entitySpawnDesign.EntityDesign) >= entitySpawnDesign.MaximumCountInLevel)
                return false;

            return true;
        }
    }
}