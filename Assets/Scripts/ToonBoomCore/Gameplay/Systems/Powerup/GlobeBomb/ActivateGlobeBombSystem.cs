using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Gameplay.Systems.Powerup.Globe;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Powerup.GlobeBomb
{
    public class ActivateGlobeBombSystem : ActivateGlobeSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public override void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            if (powerUpEntity is Entities.Powerup.GlobeBomb.GlobeBomb globeBomb)
            {
                List<int> nodesOfColor = GetAllNodesWithBlocksOfColor(levelState, globeBomb.BlockColor);
                CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState,powerUpEntity.GetIndex());
                for (int i = 0; i < nodesOfColor.Count; i++)
                {
                    CoreSystemReferenceHandler.Instance.ActivatePowerupSystem.ActivateBombSystem.ActivateBomb(levelState,nodesOfColor[i],globeBomb.Range);
                }
            }
        }
    }
}