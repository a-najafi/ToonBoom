using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Life
{
    // an entity with life will lose 1 life when cleared
    public interface ILifeEntity : IGridNodeEntity
    {
        int GetCurrentLife { get; }
        
        void ModifyLife(int amount);
        
    }
}