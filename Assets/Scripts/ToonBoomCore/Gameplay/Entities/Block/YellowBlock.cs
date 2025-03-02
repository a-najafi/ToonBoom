using System;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities
{
    
    [Serializable]
    public class YellowBlock : BlockEntity
    {
        public YellowBlock()
        {
            this.blockColor = IBlockEntity.EBlockColor.Yellow;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/YellowBlock");
            
        }
        
        public YellowBlock(int index)
        {
            this.blockColor = IBlockEntity.EBlockColor.Yellow;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/YellowBlock");
            this.SetIndex(index);
        }
        public override IGridNodeEntity GetCopy()
        {
            return new YellowBlock(GetIndex());
        }
    }
}