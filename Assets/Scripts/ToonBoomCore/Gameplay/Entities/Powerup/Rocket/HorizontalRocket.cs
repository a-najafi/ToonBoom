using System;
using ToonBoomCore.Gameplay.Entities.Concrete;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Powerup.Rocket
{
    [Serializable]
    public class HorizontalRocket : PowerupEntity
    {

        public HorizontalRocket(HorizontalRocket horizontalRocket) : base(horizontalRocket)
        {
            
        }


        public HorizontalRocket(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 1;
            score = 20;
        }

        public HorizontalRocket()
        {
            prefabVisualizerPath.SetPath("Visualizers/HorizontalRocket");
            powerPriority = 1;
            score = 20;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new HorizontalRocket(this);
        }
        
      
    }
}