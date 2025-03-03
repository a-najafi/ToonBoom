using System.Collections.Generic;
using ToonBoomCore.Grid;
using ToonBoomCore.Grid.Basic;
using ToonBoomCore.Level.Design;
using UnityEngine;

namespace ToonBoomCore.Player.Design
{
    public interface IPlayerDesign
    {
        List<ILevelDesign> LevelsToPlay { get; }
    }

   


}