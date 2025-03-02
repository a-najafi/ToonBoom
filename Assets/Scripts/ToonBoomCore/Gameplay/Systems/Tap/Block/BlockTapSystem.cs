using System;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Systems
{
    
    
    
    
    
    public class BlockTapSystem : TapSystemBase
    {
        protected BlockTapSystemConfiguration blockTapSystemConfiguration = null;
        
        
        
        
        public override void Initialize(ILevelState levelState)
        {
            blockTapSystemConfiguration = Resources.Load<BlockTapSystemConfiguration>("BlockTapSystemConfiguration");
             
            
        }

        public override void TapNode(ILevelState levelState, int nodeIndex)
        {
            IGridState gridState = levelState.GetGridState();
            //detect if tapped node contains a block
            if(!CheckForBlock(gridState,nodeIndex))
                return;

            //get the chain of blocks that will be cleared
            List<int> blockChain = GetBlockChain(gridState, nodeIndex);
            
            //there has to be atleast 2 blocks of same color in chain
            if(blockChain.Count < 2)
                return;
            
            //detect  if a powerup will be added to board as a result of the chain of blocks being cleared and identify it
            PowerupEntity powerupEntity = GetPowerup(blockChain, nodeIndex);

            for (int i = 0; i < blockChain.Count; i++)
            {
                CoreSystemReferenceHandler.Instance.ClearNodeSystem.ClearNode(gridState, blockChain[i]);
            }

            if (powerupEntity != null)
            {
                IGridNodeEntity powerUpInstance =
                    CoreSystemReferenceHandler.Instance.EntityPoolSystem.GetNewInstanceOf(powerupEntity);
            
                CoreSystemReferenceHandler.Instance.EntityOnGridSystem.AddEntityToGridAt(gridState,powerUpInstance,nodeIndex);

            }
            
            CoreSystemReferenceHandler.Instance.GravitySystem.ApplyGravity(gridState);

            CoreSystemReferenceHandler.Instance.RefillSystem.Refill(levelState);

        }

        public PowerupEntity GetPowerup(List<int> blockChain, int nodeIndex)
        {
            List<int> chainNumberKeys = blockTapSystemConfiguration.ComboNumberToPowerup.ToDictionary().Keys.ToList();
            int powerupChainNumber = 0;

            for (int i = 0; i < chainNumberKeys.Count; i++)
            {
                if (chainNumberKeys[i] > powerupChainNumber && blockChain.Count >= chainNumberKeys[i])
                    powerupChainNumber = chainNumberKeys[i];
            }

            blockTapSystemConfiguration.ComboNumberToPowerup.ToDictionary().TryGetValue(powerupChainNumber, out PowerupEntity powerupEntity);
            return powerupEntity;
        }
        
        public bool CheckForBlock(IGridState gridState, int nodeIndex)
        {
            IGridNode node = gridState.GetNodeAt(nodeIndex);
            return node.GetEntities().Exists((entity => entity is BlockEntity));
        }
        
        
        public List<int> GetBlockChain(IGridState gridState, int nodeIndex)
        {
            IGridNode sourceNode = gridState.GetNodeAt(nodeIndex);
            BlockEntity sourceBlockEntity=  sourceNode.GetEntities().First((entity => entity is BlockEntity)) as BlockEntity;
            if(sourceBlockEntity == null)
                throw new ArgumentNullException(nameof(sourceBlockEntity));
            
            IBlockEntity.EBlockColor blockColor = sourceBlockEntity.BlockColor;
            return gridState.GetNodeChain(nodeIndex, (_nodeIndex) =>
            {
                if (gridState == null) throw new ArgumentNullException(nameof(gridState));
                return gridState.GetNodeAt(_nodeIndex).GetEntities().Exists((entity =>
                {
                    var blockEntity = entity as BlockEntity;
                    return blockEntity != null && blockEntity.BlockColor == blockColor;
                }));
            });
            
        }
    }
}