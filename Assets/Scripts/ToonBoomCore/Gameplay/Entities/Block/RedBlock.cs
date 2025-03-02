using System;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities
{
    [Serializable]
    public class RedBlock : BlockEntity
    {
        public RedBlock()
        {
            this.blockColor = IBlockEntity.EBlockColor.Red;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/RedBlock");
            
        }
        
        public RedBlock(int index)
        {
            this.blockColor = IBlockEntity.EBlockColor.Red;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/RedBlock");
            this.SetIndex(index);
        }
        public override IGridNodeEntity GetCopy()
        {
            return new RedBlock(GetIndex());
        }
    }
}