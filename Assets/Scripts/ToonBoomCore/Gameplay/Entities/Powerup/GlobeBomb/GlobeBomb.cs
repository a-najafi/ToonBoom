using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Powerup.GlobeBomb
{
    public class GlobeBomb : PowerupEntity
    {
        
        public GlobeBomb(GlobeBomb globe) : base(globe)
        {
            
        }

        public GlobeBomb(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 3;
            score = 120;
        }

        public GlobeBomb()
        {
            prefabVisualizerPath.SetPath("Visualizers/GlobeBomb");
            powerPriority = 3;
            score = 120;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new GlobeBomb(this);
        }
    }
}