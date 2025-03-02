using System;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Entities.Moving;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design;
using ToonBoomCore.Level.State;
using Random = UnityEngine.Random;

namespace ToonBoomCore.Gameplay.Systems.Refill
{
    public class RefillSystem : GameSystem
    {
        
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void RefillColumn(ILevelState levelState, int x)
        {
            IGridState gridState = levelState.GetGridState();
            int numberOfRows = gridState.GetBoundsHeight();


            // starting from the top row we will find any nodes that have no collision meaning they are empty
            for (int y = numberOfRows - 1; y >= 0; y--)
            {
                int index = y * gridState.GetBoundsWidth() + x;
                List<IGridNodeEntity> entities = gridState.GetNodeAt(index).GetEntities();
                bool isAvailable = true;
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i] is ICollisionEntity)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (!isAvailable)
                    break;

                IGridNodeEntity entityToSpawn = GetNextEntityToRefill(levelState);

                IGridNodeEntity newEntityInstance =
                    CoreSystemReferenceHandler.Instance.EntityPoolSystem.GetNewInstanceOf(entityToSpawn);

                CoreSystemReferenceHandler.Instance.EntityOnGridSystem.AddEntityToGridAt(gridState, newEntityInstance, index);

            }
        }

        public IGridNodeEntity GetNextEntityToRefill(ILevelState levelState)
        {
            ILevelDesign levelDesign = levelState.LevelDesign;
            float totalProbability = 0;
            List<IEntitySpawnDesign> spawnDesigns = new List<IEntitySpawnDesign>();
            for (int i = 0; i < levelDesign.EntitySpawnDesigns.Count; i++)
            {
                if (CoreSystemReferenceHandler.Instance.EntityPoolSystem.CheckEntitySpawnDesignRequirements(
                        levelDesign.EntitySpawnDesigns[i]))
                {
                    spawnDesigns.Add(levelDesign.EntitySpawnDesigns[i]);
                    totalProbability += levelDesign.EntitySpawnDesigns[i].SpawnProbability;
                }
            }
            
            //currently useless has conflict with probability value
            //spawnDesigns.Sort((sd1,sd2) => sd1.SpawnProbability.CompareTo(sd2.SpawnProbability));
            
            float randomRoll =Random.Range(0, totalProbability);
            float totalRoll = 0f;
            for (int i = 0; i < spawnDesigns.Count(); i++)
            {
                totalRoll += spawnDesigns[i].SpawnProbability;
                if (randomRoll <= totalRoll)
                {
                    return spawnDesigns[i].EntityDesign;
                }
            }

            throw new Exception("no possible entity spawn based on rules found");
            return null;
            
        }

        

        public void Refill(ILevelState levelState)
        {
            IGridState gridState = levelState.GetGridState();
            int numberOfColumns = gridState.GetBoundsWidth();
            
            //we will apply gravity for each column
            for (int x = 0; x < numberOfColumns; x++)
            {
                RefillColumn(levelState, x);

            }
        }
    }
}