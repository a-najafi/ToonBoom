using System.Linq;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EventSequencer.Events;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace ToonBoomCore.Gameplay.Systems.Grid
{
    public class EntityOnGridSystem : GameSystem
    {
        
        public override void Initialize(ILevelState levelState)
        {
        }

        public void AddEntityToGridAt(ILevelState levelState, IGridNodeEntity gridNodeEntity, int index)
        {

            IGridState gridState = levelState.GetGridState();
            if (gridNodeEntity is ICollisionEntity && gridState.GetNodeAt(index).GetEntities().Exists(entity => entity is ICollisionEntity))
            {
                IGridNodeEntity alreadyExists =
                    gridState.GetNodeAt(index).GetEntities().First(entity => entity is ICollisionEntity);
                Debug.Log("trying to add " + gridNodeEntity.GetType().Name + " to " + index +" but " + alreadyExists.GetType().Name);
                
                throw new System.Exception("Cannot add collision entity to a node that already contains another collision entity.");
            }
            
            gridNodeEntity.SetIndex(index);
            gridState.GetNodeAt(index).AddEntity(gridNodeEntity);
            CoreSystemReferenceHandler.Instance.EventSystem.QueuEvent(levelState, new SpawnEvent(levelState.GetTimeStamp(), gridNodeEntity,index));
            
            
        }

        public void RemoveEntityFromGridAt(ILevelState levelState, IGridNodeEntity gridNodeEntity, int index)
        {
            IGridState gridState = levelState.GetGridState();
            CoreSystemReferenceHandler.Instance.EventSystem.QueuEvent(levelState, new DestroyedEvent(levelState.GetTimeStamp(), gridNodeEntity,index));

            gridState.GetNodeAt(index).RemoveEntity(gridNodeEntity);
        }
    }
}