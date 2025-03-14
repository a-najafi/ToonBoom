using System.Collections.Generic;
using ToonBoomCore.Level.Design;
using UnityEngine;

namespace ToonBoomCore.Player.Design
{
    [CreateAssetMenu(fileName = "PlayerDesign", menuName = "Config/PlayerDesign")]
    public class SO_PlayerDesign : ScriptableObject, IPlayerDesign
    {
        [SerializeField] private List<SO_LevelDesign> levelDesigns = new List<SO_LevelDesign>();

        private List<ILevelDesign> levelDesignsToPlay;
        public List<ILevelDesign> LevelsToPlay => levelDesignsToPlay ??= levelDesigns.ConvertAll(levelDesign => levelDesign as ILevelDesign);


    }
}