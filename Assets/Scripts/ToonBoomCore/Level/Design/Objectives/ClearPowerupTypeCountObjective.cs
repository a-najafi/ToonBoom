using System;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Level.Design.Objectives
{
    [Serializable]
    public class ClearPowerupTypeCountObjective : Objective
    {
        
        [SerializeField]private SerializedType<PowerupEntity> _powerupType;
        
        public static string Name = "ClearPowerupTypeCount";
        public static string Description = "Clear Number of Powerups of certain Type";
        
        public static string MakeCombinedName(Type _powerupType)
        {
            return Name + _powerupType.Name;
        }
        
        public ClearPowerupTypeCountObjective()
        {
            objectiveName = Name;
            objectiveDescription = Description;
        }

        public override string ObjectiveName => MakeCombinedName(_powerupType.Type);
    }
}