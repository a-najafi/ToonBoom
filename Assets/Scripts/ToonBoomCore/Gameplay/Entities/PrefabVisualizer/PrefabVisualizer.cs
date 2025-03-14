using ToonBoomCore.Grid;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Gameplay.Entities.PrefabVisualizer
{
    public interface IPrefabVisualizer : IGridNodeEntity
    {
        string PrefabVisualizerPath { get; }
    }
    

}