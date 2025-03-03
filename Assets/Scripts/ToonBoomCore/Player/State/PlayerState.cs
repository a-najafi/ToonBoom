using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToonBoomCore.Player.State
{
    public interface IPlayerState
    {
        int CurrentLevel { get;}
        HashSet<int> CompletedLevels { get; }
        
        public void CompleteLevel(int level);
        
        public void SetCurrentLevel(int level);
    }

    [Serializable]
    public class PlayerState : IPlayerState
    {
        [SerializeField]private List<int> _completedLevels = new List<int>();
        [SerializeField]private int currentLevel = 0;
        
        private HashSet<int> _cachedCompletedLevels;

        public int CurrentLevel => currentLevel;
        public HashSet<int> CompletedLevels => _cachedCompletedLevels ??= new HashSet<int>(_completedLevels);
        public void CompleteLevel(int level)
        {
            _completedLevels.Add(level);
            _cachedCompletedLevels = new HashSet<int>(_completedLevels);
        }

        public void SetCurrentLevel(int level)
        {
            currentLevel = level;
        }

        public PlayerState()
        {
            currentLevel = 0;
            _completedLevels = new List<int>();
        }
    }
}