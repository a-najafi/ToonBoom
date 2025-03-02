using ToonBoomCore.Gameplay.Entities.Life;

namespace ToonBoomCore.Gameplay.Entities.ReduceLife
{
    public interface IReduceLifeOnSecondaryClear : ILifeEntity
    {
        int LifeReduction { get; }
    }
    
}