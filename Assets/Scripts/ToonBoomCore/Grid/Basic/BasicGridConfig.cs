using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ToonBoomCore.Grid.Basic
{
    [Serializable]
    public class BasicGridConfig : GridConfigBase
    {
        [SerializeField]
        private List<SerializedNode> _nodes = new List<SerializedNode>();
        
        private List<IGridNode> nodes = null;
        
        
        
        [SerializeField]
        private int boundsX = 10;
        [SerializeField]
        private int boundsY = 10;

        public List<SerializedNode> SerializedNodes => _nodes;
        
        public override List<IGridNode> Nodes => nodes ??= 
            SerializedNodes.ConvertAll(node => node as IGridNode);

        public override int GetBoundsWidth()
        {
            return boundsX;
        }

        public override int GetBoundsHeight()
        {
            return boundsY;
        }

        public override IGridState GetNewGridState()
        {
            return new BasicGridState(GetBoundsWidth(),GetBoundsHeight());
        }

        public override void ApplyConfig(ref IGridState gridState)
        {
            gridState = GetNewGridState();
        }

       
    }
}