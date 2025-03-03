using System;

namespace ToonBoomCore.Level.Design.Objectives
{
    [Serializable]
    public class ScoreObjective : Objective
    {
        public static string Name = "Score";
        public static string Description = "Reach Certain Score";

        public ScoreObjective()
        {
            objectiveName = Name;
            objectiveDescription = Description;
        }
    }
}