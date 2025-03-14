using System;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Gameplay.Entities.Powerup;
using ToonBoomCore.Gameplay.Entities.Powerup.Rocket;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Systems
{

    [Serializable]
    public class PowerUpCombos
    {
        [Serializable]
        public class RequiredPowerupType
        {
            [SerializeField]
            private SerializedType<PowerupEntity> requiredPowerupType;

            [SerializeField] private int requiredCount = 1;

            public RequiredPowerupType()
            {
                requiredPowerupType = new SerializedType<PowerupEntity>();
                requiredCount = 1;
            }

            public Type PowerupType => requiredPowerupType.Type;
            
            public int RequiredCount => requiredCount;
            
        }

        public PowerUpCombos()
        {
            powerPriority = 1;
            
        }
        
        [SerializeField]
        private int powerPriority = 1;
        
        
        [SerializeReference]
        private PowerupEntity powerUpCombo = new HorizontalRocket();
        
        [SerializeField]private List<RequiredPowerupType> powerUpTypesRequired = new List<RequiredPowerupType>();
        
        
        

        public int PowerPriority => powerPriority;
        public PowerupEntity PowerUpCombo => powerUpCombo;

        public List<RequiredPowerupType> PowerUpsRequired => powerUpTypesRequired;
    }
    
    [CreateAssetMenu(fileName = "PowerupTapSystemConfiguration", menuName = "Config/Powerup Tap System Configuration")]
    public class PowerupTapSystemConfiguration : ScriptableObject
    {
        [SerializeField]
        private List<PowerUpCombos> _powerUpPairCombos = new List<PowerUpCombos>();

        public IPowerUpEntity GetPowerUpCombo(ILevelState levelState,List<int> powerupChain)
        {
            IGridState gridState = levelState.GetGridState();
            Dictionary<Type, int> powerupTypeCounts = new Dictionary<Type, int>();
            for (int powerupChainIndex = 0; powerupChainIndex < powerupChain.Count; powerupChainIndex++)
            {
                List<IGridNodeEntity> entities = gridState.GetNodeAt(powerupChain[powerupChainIndex]).GetEntities();
                for (int entityIndex = 0; entityIndex < entities.Count; entityIndex++)
                {
                    if (entities[entityIndex] is IPowerUpEntity)
                    {
                        if(!powerupTypeCounts.ContainsKey(entities[entityIndex].GetType()))
                            powerupTypeCounts.Add(entities[entityIndex].GetType(),1);
                        else
                        {
                            powerupTypeCounts[entities[entityIndex].GetType()]++;
                        }
                    }
                }

            }
            
            _powerUpPairCombos.Sort((powerUpPairCombo1,powerUpPairCombo2) => powerUpPairCombo1.PowerPriority.CompareTo(powerUpPairCombo2.PowerPriority));

            for (int i = 0; i < _powerUpPairCombos.Count; i++)
            {
                if (_powerUpPairCombos[i].PowerUpsRequired.All(powerupType => 
                        powerupTypeCounts.ContainsKey(powerupType.PowerupType) && 
                        powerupTypeCounts[powerupType.PowerupType] >= powerupType.RequiredCount))
                {
                    return _powerUpPairCombos[i].PowerUpCombo;
                }
            }

            return null;

        }

    }
}