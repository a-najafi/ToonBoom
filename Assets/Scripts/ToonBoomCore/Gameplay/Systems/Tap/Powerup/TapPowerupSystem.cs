using System;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Systems.Powerup
{
    public class TapPowerupSystem : TapSystemBase
    {
        
        
        protected PowerupTapSystemConfiguration powerupTapSystemConfiguration = null;
        
        
        
        
        public override void Initialize(ILevelState levelState)
        {
            powerupTapSystemConfiguration = Resources.Load<PowerupTapSystemConfiguration>("PowerupTapSystemConfiguration");
        }
        

        public override void TapNode(ILevelState levelState, int nodeIndex)
        {
            IGridState gridState = levelState.GetGridState();
            //detect if tapped node contains a block
            if(!CheckForPowerup(gridState,nodeIndex))
                return;

            //get the chain of blocks that will be cleared
            List<int> powerupChain = GetPowerupChain(gridState, nodeIndex);
            
            
            //detect  if a powerup will be added to board as a result of the chain of blocks being cleared and identify it
            IPowerUpEntity powerupEntity = GetComboPowerup(levelState,powerupChain);

            if (powerupEntity != null)
            {
                for (int i = 0; i < powerupChain.Count; i++)
                {
                    CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(levelState, powerupChain[i]);
                }
                
                CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
                
                IPowerUpEntity powerUpInstance =
                    CoreSystemReferenceHandler.Instance.EntityPoolSystem.GetNewInstanceOf(powerupEntity) as IPowerUpEntity;
            
                CoreSystemReferenceHandler.Instance.EntityOnGridSystem.AddEntityToGridAt(levelState,powerUpInstance,nodeIndex);
                
                CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
                
                CoreSystemReferenceHandler.Instance.ActivatePowerupSystem.ActivatePowerup(levelState, powerUpInstance);

            }
            else
            {
                for (int i = 0; i < powerupChain.Count(); i++)
                {
                    CoreSystemReferenceHandler.Instance.ActivatePowerupSystem.ActivatePowerup(levelState, powerupChain[i]);
                }
            }
            
            CoreSystemReferenceHandler.Instance.EventSystem.IncrementTimeStamp(levelState);
            
            CoreSystemReferenceHandler.Instance.GravitySystem.ApplyGravity(levelState);
            
            CoreSystemReferenceHandler.Instance.RefillDropFromTopSystem.Refill(levelState);

        }
        
        public IPowerUpEntity GetComboPowerup(ILevelState levelState, List<int> powerupChain)
        {
            return powerupTapSystemConfiguration.GetPowerUpCombo(levelState, powerupChain);
        }
        
        
        public bool CheckForPowerup(IGridState gridState, int nodeIndex)
        {
            IGridNode node = gridState.GetNodeAt(nodeIndex);
            return node.GetEntities().Exists((entity => entity is IPowerUpEntity));
        }
        
        
        public List<int> GetPowerupChain(IGridState gridState, int nodeIndex)
        {
            IGridNode sourceNode = gridState.GetNodeAt(nodeIndex);
            IPowerUpEntity sourceBlockEntity=  sourceNode.GetEntities().First((entity => entity is IPowerUpEntity)) as IPowerUpEntity;
            if(sourceBlockEntity == null)
                throw new ArgumentNullException(nameof(sourceBlockEntity));
            
            IBlockEntity.EBlockColor blockColor = sourceBlockEntity.BlockColor;
            return gridState.GetNodeChain(nodeIndex, (_nodeIndex) =>
            {
                if (gridState == null) throw new ArgumentNullException(nameof(gridState));
                return gridState.GetNodeAt(_nodeIndex).GetEntities().Exists((entity =>
                {
                    var powerupEntity = entity as PowerupEntity;
                    return powerupEntity != null;
                }));
            });
            
        }
    }
}