using System;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Powerup.Rocket
{
    [Serializable]
    public class VerticalRocket : PowerupEntity
    {
        
        public VerticalRocket(VerticalRocket verticalRocket) : base(verticalRocket)
        {
            
        }

        public VerticalRocket(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 1;
            score = 20;
        }

        public VerticalRocket()
        {
            prefabVisualizerPath.SetPath("Visualizers/VerticalRocket");
            powerPriority = 1;
            score = 20;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new VerticalRocket(this);
        }
        
     
    }
}