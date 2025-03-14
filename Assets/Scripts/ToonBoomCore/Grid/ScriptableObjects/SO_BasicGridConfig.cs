using System;
using System.Collections.Generic;
using ToonBoomCore.Grid.Basic;
using ToonBoomCore.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace ToonBoomCore.Grid.ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "BasicGridConfig", menuName = "Config/Grid")]
    public class SO_BasicGridConfig : ScriptableObject, IGridConfig
    {
        [SerializeField] private BasicGridConfig config = new BasicGridConfig();

        private void OnValidate()
        {
            int currSize = config.GetBoundsHeight() * config.GetBoundsWidth();
            
            if (currSize != config.SerializedNodes.Count)
            {
                if (config.SerializedNodes.Count > currSize)
                {
                    Debug.Log("downsizing nodes from " + config.SerializedNodes.Count +" to " + currSize);
                    config.SerializedNodes.RemoveRange(currSize, config.SerializedNodes.Count - currSize);
                }
                else if (config.SerializedNodes.Count < currSize)
                {
                    for (int i = config.SerializedNodes.Count; i < currSize; i++)
                    {
                        config.SerializedNodes.Add(new SerializedNode(i));
                        
                    }
                }
            }
        }

        public int GetBoundsWidth()
        {
            return config.GetBoundsWidth();
        }

        public int GetBoundsHeight()
        {
            return config.GetBoundsHeight();
        }

        public List<SerializedNode> SerializedNodes => config.SerializedNodes;
        public List<IGridNode> Nodes => config.Nodes;

        public IGridState GetNewGridState()
        {
            return config.GetNewGridState();
        }

        public void ApplyConfig(ref IGridState gridState)
        {
            config.ApplyConfig(ref gridState);
        }
    }
}