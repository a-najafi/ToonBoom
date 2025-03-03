using System;

namespace ToonBoomCore.Level.Design.Objectives
{
    [Serializable]
    public class ClearPowerupCountObjective : Objective
    {
        public static string Name = "ClearPowerupCount";
        public static string Description = "Clear Number of Powerups";

        public ClearPowerupCountObjective()
        {
            objectiveName = Name;
            objectiveDescription = Description;
        }
    }
}