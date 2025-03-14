using ToonBoomCore.Level.State;

namespace ToonBoomCore.Gameplay.Systems.Objective
{
    public class ScoreObjectiveSystem : GameSystem
    {
        public override void Initialize(ILevelState levelState)
        {
            
        }

        public void ScoreObjective(ILevelState levelState, string objectiveName, int value = 1)
        {
            levelState.GainObjectiveValue(objectiveName,value);
        }
    }
}