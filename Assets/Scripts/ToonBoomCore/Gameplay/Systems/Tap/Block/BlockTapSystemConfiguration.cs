using System;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Gameplay.Entities.Powerup;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ToonBoomCore.Gameplay.Systems
{
    [CreateAssetMenu(fileName = "BlockTapSystemConfiguration", menuName = "Config/Block Tap System Configuration")]
    public class BlockTapSystemConfiguration : ScriptableObject
    {
        [Serializable]
        public class BlockComboToPowerup
        {
            [SerializeField]private int comboNumber = 6;
            
            [SerializeReference]private List<PowerupEntity> powerups = new List<PowerupEntity>();

            public int ComboNumber => comboNumber;
            public PowerupEntity GetPowerup()
            {
                return powerups[Random.Range(0, powerups.Count)];
            }

            public BlockComboToPowerup()
            {
                comboNumber = 5;
                powerups = new List<PowerupEntity>();
            }
        }
        
        [SerializeField]
        private List<BlockComboToPowerup> blockCombos = new List<BlockComboToPowerup>();

        
        public PowerupEntity GetBlockCombo(int comboNumber)
        {
            blockCombos.Sort((bc1, bc2) => bc1.ComboNumber - bc2.ComboNumber);
            int largestComboHit = -1;
            for (int i = 0; i < blockCombos.Count(); i++)
            {
                if (comboNumber >= blockCombos[i].ComboNumber)
                {
                    largestComboHit = i;
                }
                else
                {
                    break;
                }
            }

            if (largestComboHit >= 0)
                return blockCombos[largestComboHit].GetPowerup();

            return null;

        }
    }
}