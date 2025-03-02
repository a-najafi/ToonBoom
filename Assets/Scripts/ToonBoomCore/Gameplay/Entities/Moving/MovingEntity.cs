using ToonBoomCore.Gameplay.Entities.Colision;
using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Moving
{
    // an entity with moving is affected by gravity must have collision
    public interface IMovingEntity : IGridNodeEntity, ICollisionEntity
    {
        
    }
}