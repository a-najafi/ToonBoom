using System;

namespace ToonBoomCore.Level.Design.Objectives
{
    [Serializable]
    public class ClearStrawberryObjective : Objective
    {
        public static string Name = "ClearStrawberry";
        public static string Description = "Clear Number of Strawberry";

        public ClearStrawberryObjective()
        {
            objectiveName = Name;
            objectiveDescription = Description;
        }
    }
}