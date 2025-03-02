using System.Collections.Generic;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design;
using ToonBoomCore.Level.State;

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
                    CoreSystemReferenceHandler.Instance.EntityOnGridSystem.AddEntityToGridAt(levelState.GetGridState(), entity, i);
                    
                    
                }
            }
            
            CoreSystemReferenceHandler.Instance.GravitySystem.ApplyGravity(levelState.GetGridState());
                    
            CoreSystemReferenceHandler.Instance.RefillSystem.Refill(levelState);

        }
        
        
    }
}