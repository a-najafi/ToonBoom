using ToonBoomCore.Grid;

namespace ToonBoomCore.Gameplay.Entities.Score
{
    public interface IScoreEntity : IGridNodeEntity
    {
        int GetScore();
    }
}