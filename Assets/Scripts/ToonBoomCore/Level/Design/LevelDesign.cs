using System;
using System.Collections.Generic;
using ToonBoomCore.Grid;
using ToonBoomCore.Grid.Basic;
using UnityEngine;

namespace ToonBoomCore.Level.Design
{
    public interface ILevelDesign
    {
        // if player uses all moves but the objectives have not been completed the level is lost
        public int NumberOfMoves { get; }
        
        // the objectives the player must meet to complete the level
        public List<IObjective> Objectives { get; }

        // the logic of filling empty nodes with new entities
        public List<IEntitySpawnDesign> EntitySpawnDesigns{get;}

        //the setup of the level when the level starts
        public IGridConfig GridConfig { get; }
        
        
    }
    [Serializable]
    public abstract class LevelDesignBase : ILevelDesign
    {
        
        public virtual int NumberOfMoves { get; }
        public virtual List<IObjective> Objectives { get; }
        public virtual List<IEntitySpawnDesign> EntitySpawnDesigns { get; }
        public virtual IGridConfig GridConfig { get; }
    }
    
    
    [Serializable]
    public  class LevelDesign : ILevelDesign
    {

        [SerializeField]
        private int numberOfMoves = 10;

        [SerializeField] private List<Objective> objectives = new List<Objective>();
        [SerializeField] private List<EntitySpawnDesign> entitySpawnDesigns = new List<EntitySpawnDesign>();
        [SerializeField]  private BasicGridConfig gridConfig;
        
        private List<IObjective> currentObjectives = null;
        private List<IEntitySpawnDesign> currentEntitySpawnDesigns = null;
        
        public virtual int NumberOfMoves => numberOfMoves;

        public virtual List<IObjective> Objectives
        {
            get
            {
                return currentObjectives ??= objectives.ConvertAll(obj => obj as IObjective);
            }
        }

        public virtual List<IEntitySpawnDesign> EntitySpawnDesigns
        {
            get
            {
                return currentEntitySpawnDesigns ??= entitySpawnDesigns.ConvertAll(obj => obj as IEntitySpawnDesign);
            }
        }

        public virtual IGridConfig GridConfig => gridConfig;
    }
}