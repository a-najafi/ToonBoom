using System;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Gameplay.Entities.Powerup.Globe;
using ToonBoomCore.Gameplay.Entities.Powerup.GlobeBomb;
using ToonBoomCore.Gameplay.Entities.Powerup.GlobeRocket;
using ToonBoomCore.Grid;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EntityVisualizer
{
    public class GlobeVisualizer : EntityVisualizer
    {
        public Color GetColorFromBlock(IBlockEntity.EBlockColor blockColor)
        {
            switch (blockColor)
            {
                case IBlockEntity.EBlockColor.Blue:
                    return Color.blue;
                case IBlockEntity.EBlockColor.Red:
                    return Color.red;
                case IBlockEntity.EBlockColor.Yellow:
                    return Color.yellow;
                case IBlockEntity.EBlockColor.Green:
                    return Color.green;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blockColor), blockColor, null);
            }
        }

        
        public override void Initialize(IGridNodeEntity gridNodeEntity)
        {
            base.Initialize(gridNodeEntity);
            if (gridNodeEntity is Globe globe)
                _spriteRenderer.color = GetColorFromBlock(globe.BlockColor);
            else if(gridNodeEntity is GlobeBomb globeBomb)
                _spriteRenderer.color = GetColorFromBlock(globeBomb.BlockColor);
            else if(gridNodeEntity is GlobeRocket globeRocket)
                _spriteRenderer.color = GetColorFromBlock(globeRocket.BlockColor);
            
        }
    }
}