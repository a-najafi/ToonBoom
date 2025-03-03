using System;
using ToonBoomCore.Grid;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Entities.Powerup.Globe
{
    [Serializable]
    public class Globe : PowerupEntity
    {
        public Globe(Globe globe) : base(globe)
        {
            
        }

        public Globe(string prefabPath)
        {
            prefabVisualizerPath.SetPath(prefabPath);
            powerPriority = 3;
            score = 100;
        }

        public Globe()
        {
            prefabVisualizerPath.SetPath("Visualizers/Globe");
            powerPriority = 3;
            score = 100;
        }


        public override IGridNodeEntity GetCopy()
        {
            return new Globe(this);
        }

        
        
    }
}