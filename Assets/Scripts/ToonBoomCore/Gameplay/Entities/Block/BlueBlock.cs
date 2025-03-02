using System;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities
{

    [Serializable]
    public class BlueBlock : BlockEntity
    {
        

        public BlueBlock()
        {
            this.blockColor = IBlockEntity.EBlockColor.Blue;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/BlueBlock");
            
        }
        
        public BlueBlock(int index)
        {
            this.blockColor = IBlockEntity.EBlockColor.Blue;
            prefabVisualizerPath.SetPath("Visualizers/Blocks/BlueBlock");
            this.SetIndex(index);
        }
        public override IGridNodeEntity GetCopy()
        {
            return new BlueBlock(GetIndex());
        }
    }
}