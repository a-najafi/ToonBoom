using System;

namespace ToonBoomCore.Level.Design.Objectives
{
    [Serializable]
    public class ClearBlockCountObjective : Objective
    {
        public static string Name = "ClearBlockCount";
        public static string Description = "Clear Number of Blocks";

        public ClearBlockCountObjective()
        {
            objectiveName = Name;
            objectiveDescription = Description;
        }
    }
}