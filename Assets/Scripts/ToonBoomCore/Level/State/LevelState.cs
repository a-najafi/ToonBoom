using System;
using System.Collections.Generic;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design;

namespace ToonBoomCore.Level.State
{
    public interface ILevelState
    {
        ILevelDesign LevelDesign { get; }
        
        IGridState GetGridState();
        
        void GainObjectiveValue(string objectiveName, int value);
        
        int GetObjectiveValue(string objectiveName);
        
        
        int GetObjectiveRequirement(string objectiveName);
        int GetObjectiveRequirementLeft(string objectiveName);
        
        
        void UseMove();

        void IncreaseMoveCount(int value);

        bool CheckVictoryCondition();
        
        bool CheckLoseCondition();
        
        
    }
    
    
    public abstract class LevelStateBase : ILevelState
    {
        private ILevelDesign levelDesign;
        private int currentMoveCount = 0;
        
        private IGridState gridState;
        
        
        private Dictionary<string, int> objectiveRequirements = new Dictionary<string, int>();
        private Dictionary<string, int> objectiveValues = new Dictionary<string, int>();

        public LevelStateBase(ILevelDesign levelDesign)
        {
            this.levelDesign = levelDesign;
            gridState = levelDesign.GridConfig.GetNewGridState();
            currentMoveCount = levelDesign.NumberOfMoves;
            objectiveRequirements.Clear();
            foreach (var objective in levelDesign.Objectives)
            {
                objectiveRequirements.Add(objective.ObjectiveName,objective.ObjectiveValue);
                objectiveValues.Add(objective.ObjectiveName, 0);
            }
            
            
        }

        public ILevelDesign LevelDesign => levelDesign;

        public IGridState GetGridState()
        {
            return gridState;
        }

        public void GainObjectiveValue(string objectiveName, int value)
        {
            if(!objectiveRequirements.ContainsKey(objectiveName) || GetObjectiveRequirementLeft(objectiveName) <= 0)
                return; // no need to track values which are not required by level or the requirements have already been met
            if(!objectiveValues.ContainsKey(objectiveName))
                objectiveValues.Add(objectiveName, value);
            else
                objectiveValues[objectiveName] += value;
        }

        public int GetObjectiveValue(string objectiveName)
        {
            if(objectiveValues.ContainsKey(objectiveName))
                return objectiveValues[objectiveName];
            return 0;
        }

        public int GetObjectiveRequirement(string objectiveName)
        {
            if(objectiveRequirements.ContainsKey(objectiveName))
                return objectiveRequirements[objectiveName];
            return -1;
        }

        public int GetObjectiveRequirementLeft(string objectiveName)
        {
            if(objectiveRequirements.ContainsKey(objectiveName) && objectiveValues.ContainsKey(objectiveName))
                return objectiveRequirements[objectiveName] - objectiveValues[objectiveName];
            return -1;
        }

        public void UseMove()
        {
            if(currentMoveCount > 0)
                currentMoveCount--;
        }

        public void IncreaseMoveCount(int value)
        {
            currentMoveCount += value;
        }

        public bool CheckVictoryCondition()
        {
            foreach (var objectiveRequirement in objectiveRequirements)
            {
                if (GetObjectiveRequirementLeft(objectiveRequirement.Key) > 0)
                    return false;
            }

            return true;
        }

        public bool CheckLoseCondition()
        { 
            return !CheckVictoryCondition() && currentMoveCount <= 0;
        }
    }

    public class LevelState : LevelStateBase
    {
        public LevelState(ILevelDesign levelDesign) : base(levelDesign)
        {
        }
    }
}