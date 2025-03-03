using System;
using ToonBoomCore.Gameplay.Entities;
using UnityEngine;

namespace ToonBoomCore.Level.Design.Objectives
{
    [Serializable]
    public class ClearBlockColorCountObjective : Objective
    {
        
        [SerializeField] protected IBlockEntity.EBlockColor blockColor;
        
        public static string Name = "ClearBlockColorCount";
        public static string Description = "Clear Number of Blocks with Color";

        public static string MakeCombinedName(IBlockEntity.EBlockColor blockColor)
        {
            return Name + blockColor.ToString();
        }
        public ClearBlockColorCountObjective()
        {
            objectiveName = Name;
            objectiveDescription = Description;
        }

        public override string ObjectiveName => MakeCombinedName(blockColor);
    }
}