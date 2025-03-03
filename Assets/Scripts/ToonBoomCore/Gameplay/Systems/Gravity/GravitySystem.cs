using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Entities.Moving;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Gravity
{
    public class GravitySystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void ApplyGravityOnColumn(ILevelState levelState, int x)
        {
            IGridState gridState = levelState.GetGridState();
               int numberOfRows = gridState.GetBoundsHeight();
                
                
                // starting from the botom row we will find any nodes that have no collision
                for (int y = 0; y < numberOfRows; y++)
                {
                    int currIndex = (y * gridState.GetBoundsWidth()) + x;
                    List<IGridNodeEntity> entities = gridState.GetNodeAt(currIndex).GetEntities();
                    bool isAvailable = true;
                    for (int i = 0; i < entities.Count; i++)
                    {
                        if (entities[i] is ICollisionEntity)
                        {
                            isAvailable = false;
                            break;
                        }
                    }
                    
                    if(!isAvailable)
                        continue;

                    // at this point we know that this node is available so we will search for a moveable entity on higher nodes that can drop to it
                    // if we encounter a collision entity that is non moving it means that no entity can drop to this node
                    for (int lookup = y + 1; lookup < numberOfRows; lookup++)
                    {
                        int lookUpIndex = (lookup * gridState.GetBoundsWidth()) + x;
                        entities = gridState.GetNodeAt(lookUpIndex).GetEntities();
                        for (int i = 0; i < entities.Count; i++)
                        {
                            
                            // the first moving entity we find is the one that will drop to the current available node
                            if (entities[i] is IMovingEntity)
                            {
                                CoreSystemReferenceHandler.Instance.MoveSystem.MoveEntityTo(levelState,(IMovingEntity)entities[i], currIndex);
                                isAvailable = false;
                                break;
                            }
                            //if a collision entity exists that does not move it means that no entity can pass it to arrive at the current empty node
                            else if (entities[i] is ICollisionEntity)
                            {
                                isAvailable = false;
                                break;
                            }
                        }   
                        if(!isAvailable)
                            break;
                    }
                }   
        }

        public void ApplyGravity(ILevelState levelState)
        {
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            
            IGridState gridState = levelState.GetGridState();
            
            int numberOfColumns = gridState.GetBoundsWidth();
            
            //we will apply gravity for each column
            for (int x = 0; x < numberOfColumns; x++)
            {
                ApplyGravityOnColumn(levelState, x);

            }
        }
    }
}