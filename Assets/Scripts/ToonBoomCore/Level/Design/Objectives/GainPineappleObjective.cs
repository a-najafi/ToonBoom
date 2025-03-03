using System;

namespace ToonBoomCore.Level.Design.Objectives
{
    [Serializable]
    public class GainPineappleObjective : Objective
    {
        public static string Name = "GainPineapple";
        public static string Description = "Gain Number of Pinapple";

        public GainPineappleObjective()
        {
            objectiveName = Name;
            objectiveDescription = Description;
        }
    }
}