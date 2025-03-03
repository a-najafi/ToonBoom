using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Refill
{
    public class RefillDropFromTop : RefillSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public override void Refill(ILevelState levelState)
        {
            IGridState gridState = levelState.GetGridState();
            int numberOfColumns = gridState.GetBoundsWidth();

            
            bool shouldContinue = false;
            do
            {
                shouldContinue = false;
                for (int x = 0; x < numberOfColumns; x++)
                {
                    int y = gridState.GetBoundsHeight() - 1;
                    int index = (y * numberOfColumns) + x;
                    if (CheckHasCollision(gridState, index))
                        continue;
                 
                    shouldContinue = true;
                    RefillColumn(levelState, x);

                }
                
                CoreSystemReferenceHandler.Instance.GravitySystem.ApplyGravity(levelState);
                
            } while (shouldContinue);
        }

        public override void RefillColumn(ILevelState levelState, int x)
        {
            IGridState gridState = levelState.GetGridState();
            int numberOfRows = gridState.GetBoundsHeight();


            int y = numberOfRows - 1;

            int index = y * gridState.GetBoundsWidth() + x;
            
            if(CheckHasCollision(gridState,index))
                return;

            IGridNodeEntity entityToSpawn = GetNextEntityToRefill(levelState);

            IGridNodeEntity newEntityInstance =
                CoreSystemReferenceHandler.Instance.EntityPoolSystem.GetNewInstanceOf(entityToSpawn);

            CoreSystemReferenceHandler.Instance.EntityOnGridSystem.AddEntityToGridAt(levelState, newEntityInstance,
                index);


        }
    }
}