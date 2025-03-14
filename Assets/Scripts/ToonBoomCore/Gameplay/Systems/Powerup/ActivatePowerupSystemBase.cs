using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Entities.Powerup.RocketBomb;
using ToonBoomCore.Gameplay.Systems.Powerup.Bomb;
using ToonBoomCore.Gameplay.Systems.Powerup.Globe;
using ToonBoomCore.Gameplay.Systems.Powerup.GlobeBomb;
using ToonBoomCore.Gameplay.Systems.Powerup.GlobeRocket;
using ToonBoomCore.Gameplay.Systems.Powerup.RainbowGlobe;
using ToonBoomCore.Gameplay.Systems.Powerup.Rocket;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Powerup
{

    public interface IActivatePowerupSystem
    {
        void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity);
    }
    
    public abstract class ActivatePowerupSystemBase : IGameSystem , IActivatePowerupSystem
    {
        public virtual void Initialize(ILevelState levelState)
        {
            return;
        }

        public virtual void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            throw new System.NotImplementedException();
        }
    }
    
    
    public class ActivatePowerupSystem : IGameSystem , IActivatePowerupSystem
    {
        private ActivateBombSystem _activateBombSystem = new ActivateBombSystem();

        private ActivateGlobeSystem _activateGlobeSystem = new ActivateGlobeSystem();
        
        private ActivateRocketSystem _activateRocketSystem = new ActivateRocketSystem();
        
        private ActivateRocketBombSystem _activateRocketBombSystem = new ActivateRocketBombSystem();
        
        private ActivateRainbowGlobeSystem _activateRainbowGlobeSystem = new ActivateRainbowGlobeSystem();
        
        private ActivateGlobeRocketSystem _activateGlobeRocketSystem = new ActivateGlobeRocketSystem();

        private ActivateGlobeBombSystem _activateGlobeBombSystem = new ActivateGlobeBombSystem();
        

        public ActivateRocketBombSystem ActivateRocketBombSystem => _activateRocketBombSystem;
        
        public ActivateRocketSystem ActivateRocketSystem => _activateRocketSystem;
        
        public ActivateGlobeSystem ActivateGlobeSystem => _activateGlobeSystem;

        public ActivateBombSystem ActivateBombSystem => _activateBombSystem;

        public ActivateRainbowGlobeSystem ActivateRainbowGlobeSystem => _activateRainbowGlobeSystem;

        public ActivateGlobeRocketSystem ActivateGlobeRocketSystem => _activateGlobeRocketSystem;

        public ActivateGlobeBombSystem ActivateGlobeBombSystem => _activateGlobeBombSystem;

        public virtual void Initialize(ILevelState levelState)
        {
            
            _activateRocketSystem.Initialize(levelState);
            _activateBombSystem.Initialize(levelState);
            _activateGlobeSystem.Initialize(levelState);
            _activateRocketBombSystem.Initialize(levelState);
            _activateGlobeBombSystem.Initialize(levelState);
            _activateGlobeRocketSystem.Initialize(levelState);
            _activateRainbowGlobeSystem.Initialize(levelState);
            return;
        }
        
        public virtual void ActivatePowerup(ILevelState levelState, int nodeIndex)
        {
            List<IGridNodeEntity> entities = levelState.GetGridState().GetNodeAt(nodeIndex).GetEntities();
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i] is PowerupEntity powerupEntity)
                {
                    ActivatePowerup(levelState, powerupEntity);
                }
            }
           
            
        }

        public virtual void ActivatePowerup(ILevelState levelState, IPowerUpEntity powerUpEntity)
        {
            _activateRocketSystem.ActivatePowerup(levelState, powerUpEntity);
            _activateBombSystem.ActivatePowerup(levelState, powerUpEntity);
            _activateGlobeSystem.ActivatePowerup(levelState, powerUpEntity);
            _activateRocketBombSystem.ActivatePowerup(levelState,powerUpEntity);
            _activateGlobeBombSystem.ActivatePowerup(levelState, powerUpEntity);
            _activateGlobeRocketSystem.ActivatePowerup(levelState, powerUpEntity);
            _activateRainbowGlobeSystem.ActivatePowerup(levelState, powerUpEntity);
            
        }
    }
}