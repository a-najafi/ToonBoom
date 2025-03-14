using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Gameplay.Systems.Powerup.Globe;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Powerup.RainbowGlobe
{
    public class ActivateRainbowGlobeSystem : ActivateGlobeSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public override void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            if(powerUpEntity is Entities.Powerup.RainbowGlobe.RainbowGlobe rainbowGlobeEntity)
                ActivateRainbowGlobe(levelState);
            
        }
        
        public void ActivateRainbowGlobe(ILevelState levelState)
        {
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            IGridState gridState = levelState.GetGridState();
            int nodeCount = gridState.GetNodes().Count;
            for (int i = 0; i < nodeCount; i++)
            {
                CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState,i);
            }
        }
    }
}