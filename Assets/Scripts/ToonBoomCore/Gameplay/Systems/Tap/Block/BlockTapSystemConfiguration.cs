using System.Collections.Generic;
using ToonBoomCore.Gameplay.Entities.Powerup;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Systems
{
    [CreateAssetMenu(fileName = "BlockTapSystemConfiguration", menuName = "Config/Block Tap System Configuration")]
    public class BlockTapSystemConfiguration : ScriptableObject
    {
        [SerializeField]
        private SerializableDictionary<int, PowerupEntity> comboNumberToPowerup;

        public SerializableDictionary<int, PowerupEntity> ComboNumberToPowerup => comboNumberToPowerup;

    }
}