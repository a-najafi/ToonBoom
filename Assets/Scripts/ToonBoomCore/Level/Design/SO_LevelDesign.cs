using System.Collections.Generic;
using ToonBoomCore.Grid;
using ToonBoomCore.Grid.ScriptableObjects;
using UnityEngine;

namespace ToonBoomCore.Level.Design
{
    
    
    [CreateAssetMenu(fileName = "LevelDesign", menuName = "Config/Level")]
    public class SO_LevelDesign : ScriptableObject, ILevelDesign
    {
        [SerializeField]
        private int numberOfMoves = 10;

        [SerializeReference] private List<Objective> objectives = new List<Objective>();
        [SerializeField] private List<EntitySpawnDesign> entitySpawnDesigns = new List<EntitySpawnDesign>();
        [SerializeField] private SO_BasicGridConfig gridConfig;
        
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