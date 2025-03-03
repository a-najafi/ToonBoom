using System.Collections.Generic;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Gameplay.Systems.Powerup;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Entities.Powerup.RocketBomb
{
    public class ActivateRocketBombSystem : ActivatePowerupSystemBase
    {
        public override void Initialize(ILevelState levelState)
        {
            base.Initialize(levelState);
        }

        public override void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {

            if (powerUpEntity is RocketBomb rocketBomb)
            {
                int nodeIndex = powerUpEntity.GetIndex();
                int range = rocketBomb.Range;

                ActivateRocketBomb(levelState, nodeIndex, range);
            }
        }


        public void ActivateRocketBomb(ILevelState levelState, int nodeIndex, int range = -1)
        {

            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            IGridState gridState = levelState.GetGridState();

            range = range >= 0 ? range : Mathf.Max(gridState.GetBoundsHeight(), gridState.GetBoundsWidth());

            List<int> aoeNodes = gridState.GetSquareAOE(nodeIndex, range);

            HashSet<int> xVerticals = new HashSet<int>();
            HashSet<int> yVerticals = new HashSet<int>();

            for (int i = 0; i < aoeNodes.Count; i++)
            {
                int aoeNodeX = aoeNodes[i] % gridState.GetBoundsWidth();
                int aoeNodeY = aoeNodes[i] / gridState.GetBoundsWidth();

                if (!xVerticals.Contains(aoeNodeX))
                {
                    CoreSystemReferenceHandler.Instance.ActivatePowerupSystem.ActivateRocketSystem.ActivateVerticalRocket(levelState,
                        aoeNodes[i]);
                    xVerticals.Add(aoeNodeX);
                }

                if (!yVerticals.Contains(aoeNodeY))
                {
                    CoreSystemReferenceHandler.Instance.ActivatePowerupSystem.ActivateRocketSystem.ActivateVerticalRocket(levelState,
                        aoeNodes[i]);
                    yVerticals.Add(aoeNodeY);
                }

                CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, aoeNodes[i]);

            }
        }

    }
}