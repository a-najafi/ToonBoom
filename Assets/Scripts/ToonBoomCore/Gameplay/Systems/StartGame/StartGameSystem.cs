using System.Collections.Generic;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EventSequencer.Events;

namespace ToonBoomCore.Gameplay.Systems.StartGame
{
    public class StartGameSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
        }

        public void StartGame(ref ILevelState levelState)
        {

            List<IGridNode> nodes = levelState.LevelDesign.GridConfig.Nodes;


            for (int i = 0; i < nodes.Count; i++)
            {
                List<IGridNodeEntity> entities = nodes[i].GetEntities(); 
                for (int j = 0; j < entities.Count; j++)
                {
                    IGridNodeEntity entity =
                        CoreSystemReferenceHandler.Instance.EntityPoolSystem.GetNewInstanceOf(entities[j]);
                    CoreSystemReferenceHandler.Instance.EntityOnGridSystem.AddEntityToGridAt(levelState, entity, i);
                }
            }
            
            CoreSystemReferenceHandler.Instance.GravitySystem.ApplyGravity(levelState);
                    
            CoreSystemReferenceHandler.Instance.RefillSystem.Refill(levelState);
            
            
            CoreSystemReferenceHandler.Instance.EventSystem.QueuEvent(levelState,new ObjectiveScoredEvent(levelState.GetTimeStamp(),levelState));

        }
        
        
    }
}