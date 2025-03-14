using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Powerup.Bomb
{
    public class ActivateBombSystem : ActivatePowerupSystemBase
    {
        public override void Initialize(ILevelState levelState)
        {
            base.Initialize(levelState);
        }

        public override void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            if (powerUpEntity is Entities.Powerup.Bomb.Bomb)
            {
                ActivateBomb(levelState, powerUpEntity as Entities.Powerup.Bomb.Bomb);    
            }
        }

        public virtual void ActivateBomb(ILevelState levelState, Entities.Powerup.Bomb.Bomb powerup)
        {
            IGridState gridState = levelState.GetGridState();

            int range = powerup.Range >= 0 ? powerup.Range : gridState.GetBoundsHeight();
            
            int nodeIndex = powerup.GetIndex();


            ActivateBomb(levelState, nodeIndex, range);
            
            
            
        }
        
        public virtual void ActivateBomb(ILevelState levelState, int nodeIndex, int range)
        {
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            IGridState gridState = levelState.GetGridState();

            
            int x = nodeIndex % gridState.GetBoundsWidth();
            int y = nodeIndex / gridState.GetBoundsWidth();

            List<int> aoeNodes = levelState.GetGridState().GetSquareAOE(nodeIndex, range);

            for (int i = 0; i < aoeNodes.Count; i++)
            {
                CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, aoeNodes[i]);    
            }
            
        }
        
        
    }
}