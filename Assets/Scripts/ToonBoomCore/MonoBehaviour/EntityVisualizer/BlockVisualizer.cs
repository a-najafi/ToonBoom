using System;
using DG.Tweening;
using ToonBoomCore.Gameplay.Entities;
using ToonBoomCore.Grid;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EntityVisualizer
{
    public class BlockVisualizer : EntityVisualizer
    {
     
        public Color GetColorFromBlock(IBlockEntity.EBlockColor blockColor)
        {
            switch (blockColor)
            {
                case IBlockEntity.EBlockColor.Blue:
                    return Color.blue;
                    break;
                case IBlockEntity.EBlockColor.Red:
                    return Color.red;
                    break;
                case IBlockEntity.EBlockColor.Yellow:
                    return Color.yellow;
                    break;
                case IBlockEntity.EBlockColor.Green:
                    return Color.green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blockColor), blockColor, null);
            }
        }

        public override void Initialize(IGridNodeEntity gridNodeEntity)
        {
            transform.localScale = Vector3.zero;
            if (gridNodeEntity is BlockEntity blockEntity)
            {
                //_spriteRenderer.color = GetColorFromBlock(blockEntity.BlockColor);
                transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutBounce);
            }
            else
            {
                throw new System.Exception("Expected BlockEntity");
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}