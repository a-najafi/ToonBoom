using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EventSequencer.Events;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine.Events;

namespace ToonBoomCore.Gameplay.Systems.Move
{
    public class MoveSystem : GameSystem
    {
        
        
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void MoveEntityTo(ILevelState levelState, IGridNodeEntity gridNodeEntity, int to)
        {
            IGridState gridState = levelState.GetGridState();
            int from = gridNodeEntity.GetIndex();
            CoreSystemReferenceHandler.Instance.EventSystem.QueuEvent(levelState, new FallEvent(levelState.GetTimeStamp(),gridNodeEntity,from,to));
            
            gridState.GetNodeAt(gridNodeEntity.GetIndex()).RemoveEntity(gridNodeEntity);
            
            gridNodeEntity.SetIndex(to);
            
            gridState.GetNodeAt(to).AddEntity(gridNodeEntity);
        }
    }
}