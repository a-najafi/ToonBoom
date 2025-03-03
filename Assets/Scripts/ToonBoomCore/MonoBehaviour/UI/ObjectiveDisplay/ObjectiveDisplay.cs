using System.Linq;
using ToonBoomCore.Level.Design;
using ToonBoomCore.Level.State;
using UnityEngine;
using UnityEngine.UI;

namespace ToonBoomCore.MonoBehaviour.UI.ObjectiveDisplay
{
    public class ObjectiveDisplay : UnityEngine.MonoBehaviour
    {
        [SerializeField]private Text text;

        public void UpdateObjectiveDisplay(ILevelState levelState)
        {
            text.text = "";
            ILevelDesign levelDesign = levelState.LevelDesign;
            string finalDisplay = "0. Number of Moves Left: " + levelState.MoveCountLeft;
            for (int i = 0; i < levelDesign.Objectives.Count; i++)
            {
                string objectiveText = "1. " + levelDesign.Objectives[i].ObjectiveName + "\n" +
                                        "   Description: " + levelDesign.Objectives[i].ObjectiveDescription + "\n" +
                                        "   Status: " + levelState.GetObjectiveValue(levelDesign.Objectives[i].ObjectiveName) + " / " + levelDesign.Objectives[i].ObjectiveValue;
                finalDisplay += "\n" + objectiveText;

            }
            text.text = finalDisplay;
        }
    }
}