using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Powerup.GlobeRocket
{
    public class GlobeRocket : PowerupEntity
    {
        public GlobeRocket(GlobeRocket globe) : base(globe)
        {
            
        }

        public GlobeRocket(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 3;
            score = 160;
        }

        public GlobeRocket()
        {
            prefabVisualizerPath.SetPath("Visualizers/GlobeRocket");
            powerPriority = 3;
            score = 160;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new GlobeRocket(this);
        }

     
    }
}