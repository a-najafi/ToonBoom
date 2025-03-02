using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine.Events;

namespace ToonBoomCore.Gameplay.Systems.Move
{
    public class MoveSystem : GameSystem
    {
        
        public UnityEvent<IGridNodeEntity, int, int> OnEntityMoveOnGrid = new UnityEvent<IGridNodeEntity, int, int>();
        
        public override void Initialize(ILevelState levelState)
        {
            OnEntityMoveOnGrid.RemoveAllListeners();
        }

        public void MoveEntityTo(IGridState gridState, IGridNodeEntity gridNodeEntity, int to)
        {
            int from = gridNodeEntity.GetIndex();
            OnEntityMoveOnGrid.Invoke(gridNodeEntity, from, to);
            gridState.GetNodeAt(gridNodeEntity.GetIndex()).RemoveEntity(gridNodeEntity);
            
            gridNodeEntity.SetIndex(to);
            
            gridState.GetNodeAt(to).AddEntity(gridNodeEntity);
        }
    }
}