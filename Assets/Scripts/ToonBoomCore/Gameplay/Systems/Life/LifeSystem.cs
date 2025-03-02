using ToonBoomCore.Gameplay.Entities.Life;
using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Life
{
    public class LifeSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public virtual void IncreaseLife(ILifeEntity lifeEntity, int amount)
        {
            
        }
        
        public virtual void DecreaseLife(ILifeEntity lifeEntity, int amount)
        {
            
        }
    }
}