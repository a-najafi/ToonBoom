using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Gameplay.Systems.Powerup.Globe;
using ToonBoomCore.Level.State;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Systems.Powerup.GlobeRocket
{
    public class ActivateGlobeRocketSystem : ActivateGlobeSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            base.Initialize(levelState);
        }

        public override void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            if (powerUpEntity is Entities.Powerup.GlobeRocket.GlobeRocket globeRocket)
            {
               ActivateGlobeRocket(levelState,powerUpEntity.GetIndex(), globeRocket.BlockColor);
            }
        }

        public void ActivateGlobeRocket(ILevelState levelState, int nodeIndex, IBlockEntity.EBlockColor color)
        {
            CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, nodeIndex);
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            List<int> nodesOfColor = GetAllNodesWithBlocksOfColor(levelState, color);
            for (int i = 0; i < nodesOfColor.Count; i++)
            {
                int randomRocket = Random.Range(0, 2);
                if(randomRocket == 0)
                    CoreSystemReferenceHandler.Instance.ActivatePowerupSystem.ActivateRocketSystem.ActivateVerticalRocket(levelState, nodesOfColor[i]);
                else
                    CoreSystemReferenceHandler.Instance.ActivatePowerupSystem.ActivateRocketSystem.ActivateHorizontalRocket(levelState, nodesOfColor[i]);
            }
        }
    }
}