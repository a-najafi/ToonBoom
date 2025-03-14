using System;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Powerup.Bomb
{
    [Serializable]
    public class Bomb : PowerupEntity
    {
        public Bomb(Bomb bomb) : base(bomb)
        {
            
        }
        public Bomb(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 2;
            score = 60;
        }

        public Bomb()
        {
            prefabVisualizerPath.SetPath("Visualizers/Bomb");
            powerPriority = 2;
            score = 60;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new Bomb(this);
        }
        
      
    }
}