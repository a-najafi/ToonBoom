using ToonBoomCore.Gameplay.Systems.Powerup;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Entities.Powerup.RocketBomb
{
    public class RocketBomb : PowerupEntity
    {
        
        public RocketBomb(RocketBomb rocketBomb) : base(rocketBomb)
        {
            
        }

        public RocketBomb(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 2;
            score = 120;
        }

        public RocketBomb()
        {
            prefabVisualizerPath.SetPath("Visualizers/RocketBomb");
            powerPriority = 2;
            score = 120;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new RocketBomb(this);
        }
    }
}