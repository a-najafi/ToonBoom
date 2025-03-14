using System;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Powerup.Bomb
{
    [Serializable]
    public class BigBomb : Bomb
    {
        public BigBomb(BigBomb bomb) : base(bomb)
        {
            
        }
        public BigBomb(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 2;
            score = 100;
        }

        public BigBomb()
        {
            prefabVisualizerPath.SetPath("Visualizers/BigBomb");
            powerPriority = 2;
            score = 100;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new BigBomb(this);
        }
        
      
    }
}