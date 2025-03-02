using System.Linq;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace ToonBoomCore.Gameplay.Systems.Grid
{
    public class EntityOnGridSystem : GameSystem
    {
        public UnityEvent<IGridNodeEntity, int> OnEntityAdded = new UnityEvent<IGridNodeEntity, int>();
        public UnityEvent<IGridNodeEntity, int> OnEntityRemoved = new UnityEvent<IGridNodeEntity, int>();
        
        public override void Initialize(ILevelState levelState)
        {
            OnEntityAdded.RemoveAllListeners();
            OnEntityRemoved.RemoveAllListeners();
        }

        public void AddEntityToGridAt(IGridState gridState, IGridNodeEntity gridNodeEntity, int index)
        {

            if (gridNodeEntity is ICollisionEntity && gridState.GetNodeAt(index).GetEntities().Exists(entity => entity is ICollisionEntity))
            {
                IGridNodeEntity alreadyExists =
                    gridState.GetNodeAt(index).GetEntities().First(entity => entity is ICollisionEntity);
                Debug.Log("trying to add " + gridNodeEntity.GetType().Name + " to " + index +" but " + alreadyExists.GetType().Name);
                
                throw new System.Exception("Cannot add collision entity to a node that already contains another collision entity.");
            }
            
            gridNodeEntity.SetIndex(index);
            gridState.GetNodeAt(index).AddEntity(gridNodeEntity);
            OnEntityAdded.Invoke(gridNodeEntity, index);
        }

        public void RemoveEntityFromGridAt(IGridState gridState, IGridNodeEntity gridNodeEntity, int index)
        {
            OnEntityRemoved.Invoke(gridNodeEntity, index);
            gridState.GetNodeAt(index).RemoveEntity(gridNodeEntity);
        }
    }
}