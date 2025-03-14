using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Entities.Powerup.Rocket;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Powerup.Rocket
{
    public class ActivateRocketSystem : ActivatePowerupSystemBase
    {
        public override void Initialize(ILevelState levelState)
        {
            base.Initialize(levelState);
        }

        public override void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            if (powerUpEntity is VerticalRocket)
            {
                ActivateVerticalRocket(levelState,powerUpEntity);
            }
            else if (powerUpEntity is HorizontalRocket)
            {
                ActivateHorizontalRocket(levelState, powerUpEntity);
            }
        }

        public void ActivateVerticalRocket(ILevelState levelState, IPowerUpEntity powerup)
        {
            IGridState gridState = levelState.GetGridState();

            int range = powerup.Range >= 0 ? powerup.Range : gridState.GetBoundsHeight();
            
            int nodeIndex = powerup.GetIndex();
            
           ActivateVerticalRocket(levelState,nodeIndex,range);
           
        }
        
        public void ActivateVerticalRocket(ILevelState levelState, int nodeIndex, int range = -1)
        {
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            IGridState gridState = levelState.GetGridState();

            range = range >= 0 ? range : gridState.GetBoundsHeight();
            
            
            int x = nodeIndex % gridState.GetBoundsWidth();
            int y = nodeIndex / gridState.GetBoundsWidth();
            CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, nodeIndex);
            for (int i = 0; i < range; i++)
            {
                int upperY = y + i;
                int upperIndex = (upperY * gridState.GetBoundsWidth()) + x;
                if(gridState.IsValidNodeLocation(x,upperY))
                    CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, upperIndex);
                
                int lowerY = y - i;
                int lowerIndex = (lowerY * gridState.GetBoundsWidth()) + x;
                
                if(gridState.IsValidNodeLocation(x,lowerY) )
                    CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, lowerIndex);
                
                
            }
        }

        public void ActivateHorizontalRocket(ILevelState levelState, int nodeIndex, int range = -1)
        {
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            IGridState gridState = levelState.GetGridState();
            
            
            int x = nodeIndex % gridState.GetBoundsWidth();
            int y = nodeIndex / gridState.GetBoundsWidth();
            CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, nodeIndex);
            for (int i = 0; i < range ; i++)
            {
                int rightX = x + i;
                int rightIndex = (y * gridState.GetBoundsWidth()) + rightX;
                if(gridState.IsValidNodeLocation(rightX,y) )
                    CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, rightIndex);
                
                int leftX = x - i;
                int leftIndex = (y * gridState.GetBoundsWidth()) + leftX;
                if(gridState.IsValidNodeLocation(leftX,y) )
                    CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, leftIndex);
                
            }
        }

        public void ActivateHorizontalRocket(ILevelState levelState, IPowerUpEntity powerup)
        {
            
            IGridState gridState = levelState.GetGridState();
            
            int nodeIndex = powerup.GetIndex();
            int range = powerup.Range >= 0 ? powerup.Range : gridState.GetBoundsWidth();
            ActivateHorizontalRocket(levelState, nodeIndex, range);
            

        }
        
    }
}