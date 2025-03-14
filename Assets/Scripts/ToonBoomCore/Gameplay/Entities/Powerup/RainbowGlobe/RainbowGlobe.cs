using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Powerup.RainbowGlobe
{
    public class RainbowGlobe : PowerupEntity
    {
        public RainbowGlobe(RainbowGlobe rainbowGlobe) : base(rainbowGlobe)
        {
            
        }
        
        public RainbowGlobe(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 3;
            score = 200;
        }

        public RainbowGlobe()
        {
            prefabVisualizerPath.SetPath("Visualizers/RainbowGlobe");
            powerPriority = 3;
            score = 200;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new RainbowGlobe(this);
        }
    }
}