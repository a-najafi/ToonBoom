using System;
using UnityEngine;

namespace ToonBoomCore.Level.Design
{
    public interface IObjective
    {
        
        int ObjectiveValue { get; }
        
        string ObjectiveName { get; }
        
        string ObjectiveDescription { get; }
    }

    [Serializable]
    public abstract class ObjectiveBase : IObjective
    {
        public virtual int ObjectiveValue { get; }
        public virtual string ObjectiveName { get; }
        public virtual string ObjectiveDescription { get; }
    }

    [Serializable]
    public class Objective : ObjectiveBase
    {
        [SerializeField] private int objectiveValue;
        [SerializeField] private string objectiveName;
        [SerializeField] private string objectiveDescription;
        
        public override int ObjectiveValue => objectiveValue;
        public override string ObjectiveName => objectiveName;
        public override string ObjectiveDescription => objectiveDescription;
    }
}