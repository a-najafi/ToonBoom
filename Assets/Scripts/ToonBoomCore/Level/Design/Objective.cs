using System;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Level.Design
{
    public interface IObjective
    {
        
        int ObjectiveValue { get; }
        
        string ObjectiveName { get; }
        
        string ObjectiveDescription { get; }
        
        public Sprite ObjectiveIcon { get; }
    }

    [Serializable]
    public abstract class ObjectiveBase : IObjective
    {

        public ObjectiveBase()
        {
            
        }
        public virtual int ObjectiveValue { get; }
        public virtual string ObjectiveName { get; }
        public virtual string ObjectiveDescription { get; }
        public virtual Sprite ObjectiveIcon { get; }
    }

    [Serializable]
    public class Objective : ObjectiveBase
    {
        [SerializeField] protected int objectiveValue;
        [ReadOnly][SerializeField] protected string objectiveName;
        [ReadOnly][SerializeField] protected string objectiveDescription;
        [SerializeField] protected SerializableAssetPath objectiveIconPath;
        public override int ObjectiveValue => objectiveValue;
        public override string ObjectiveName => objectiveName;
        public override string ObjectiveDescription => objectiveDescription;

        public override Sprite ObjectiveIcon => Resources.Load<Sprite>(objectiveIconPath.AssetPath);

        public Objective()
        {
            
        }
    }
}