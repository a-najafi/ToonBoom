
using System;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities
{
    
    [Serializable]
    public class GreenBlock : BlockEntity
    {
        public GreenBlock()
        {
            this.blockColor = IBlockEntity.EBlockColor.Green;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/GreenBlock");
            
        }
        
        public GreenBlock(int index)
        {
            this.blockColor = IBlockEntity.EBlockColor.Green;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/GreenBlock");
            this.SetIndex(index);
        }
        public override IGridNodeEntity GetCopy()
        {
            return new GreenBlock(GetIndex());
        }
    }
}