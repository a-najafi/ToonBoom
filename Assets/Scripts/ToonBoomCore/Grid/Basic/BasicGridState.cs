using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ToonBoomCore.Grid.Basic
{
    [Serializable]
    public class BasicGridState : IGridState
    {
        
         [SerializeField]
        private int boundsX = 10;
        [SerializeField]
        private int boundsY = 10;

        [SerializeField]
        private List<IGridNode> _gridNodes;

        int GetIndexFromLocation(int x, int y)
        {
            return y * boundsX + x;
        }

        public BasicGridState()
        {
            _gridNodes = new List<IGridNode>(GetNodeNum());
        }

        public BasicGridState(int boundsX, int boundsY)
        {
            this.boundsX = boundsX;
            this.boundsY = boundsY;
            int nodeNum = this.boundsX * this.boundsY;
            _gridNodes = new List<IGridNode>();
            for (int i = 0; i < nodeNum; i++)
            {
                _gridNodes.Add(new BasicGridNode(i,new List<IGridNodeEntity>()));
            }
        }

        public int GetNodeNum()
        {
            return GetBoundsWidth() * GetBoundsHeight();
        }

        public int GetBoundsWidth()
        {
            return boundsX;
        }

        public int GetBoundsHeight()
        {
            return boundsY;
        }

        public bool IsValidNodeIndex(int index)
        {
            return index >= 0 && index < GetNodeNum();
        }
        
        public bool IsValidX(int x)
        {
            return x >= 0 && x < GetBoundsWidth();
        }

        public bool IsValidY(int y)
        {
            return y >= 0 && y < GetBoundsHeight();
        }

        public bool IsValidNodeLocation(int x, int y)
        {
            return IsValidX(x) && IsValidY(y) && IsValidNodeIndex(GetIndexFromLocation(x, y));
        }

        public List<IGridNode> GetNodes()
        {
            return _gridNodes;
        }

        public IGridNode GetNodeAt(int index)
        {
            return _gridNodes[index];
        }

        public IGridNode GetNodeAt(int x, int y)
        {
            return GetNodeAt(GetIndexFromLocation(x, y));
        }

        public List<int> GetAdjacentNodes(int index, bool includeDiagonal = false)
        {
            List<int> adjacentNodes = new List<int>();
            
            int x = index % boundsX;
            int y = index / boundsX;
            
            
            
            if(IsValidNodeLocation( x + 1,y))
                adjacentNodes.Add(index + 1);
            if(IsValidNodeLocation( x -1,y))
                adjacentNodes.Add(index - 1);
            if(IsValidNodeLocation( x ,y+1))
                adjacentNodes.Add(index + boundsX);
            if(IsValidNodeLocation( x ,y-1))
                adjacentNodes.Add(index - boundsX);

            if (includeDiagonal)
            {
                if(IsValidNodeLocation(x + 1, y + 1))
                    adjacentNodes.Add(index + 1 + boundsX);
                if(IsValidNodeLocation( x - 1, y - 1))
                    adjacentNodes.Add(index - 1 + boundsX);
                if(IsValidNodeLocation( x + 1, y + 1))
                    adjacentNodes.Add(index + 1 - boundsX);
                if(IsValidNodeLocation( x - 1, y - 1))
                    adjacentNodes.Add(index - 1 - boundsX);
            }

            return adjacentNodes;
            
        }

        //returns aor sorted based on distance to starting node by default
        public virtual List<int> GetSquareAOE(int nodeIndex, int range)
        {
            int x = nodeIndex % GetBoundsWidth();
            int y = nodeIndex / GetBoundsWidth();
            List<int> aoeNodes = new List<int>();
            
            aoeNodes.Add(nodeIndex);
            for (int i = 1; i <= range; i++)
            {
                int upperY = y + i;
                int upperIndex = (upperY * GetBoundsWidth()) + x;
                if (IsValidNodeLocation(x, upperY))
                    aoeNodes.Add(upperIndex);

                int lowerY = y - i;
                int lowerIndex = (lowerY * GetBoundsWidth()) + x;
                if (IsValidNodeLocation(x, lowerY) )
                    aoeNodes.Add(lowerIndex);
                
                int rightX = x + i;
                int rightIndex = (y * GetBoundsWidth()) + rightX;
                if (IsValidNodeLocation(rightX, y) )
                    aoeNodes.Add(rightIndex);

                int leftX = x - i;
                int leftIndex = (y * GetBoundsWidth()) + leftX;
                if (IsValidNodeLocation(leftX, y) )
                    aoeNodes.Add(leftIndex);

                int rightUpperIndex = upperIndex + i;
                if (IsValidNodeLocation(rightX, upperY) )
                    aoeNodes.Add(rightUpperIndex);

                int leftUpperIndex = upperIndex - i;
                if (IsValidNodeLocation(leftX, upperY) )
                    aoeNodes.Add(leftUpperIndex);

                int rightLowerIndex = lowerIndex + i;
                if (IsValidNodeLocation(rightX, lowerY) )
                    aoeNodes.Add(rightLowerIndex);

                int leftLowerIndex = lowerIndex - i;
                if (IsValidNodeLocation(leftX, lowerY))
                    aoeNodes.Add(leftLowerIndex);
            }

            return aoeNodes;

        }

        public List<int> GetAdjacentNodes(int x, int y, bool includeDiagonal = false)
        {
            return GetAdjacentNodes(GetIndexFromLocation(x, y),includeDiagonal);
        }
        
        public List<int> GetNodeChain(int nodeIndex,  Func< int, bool> condition)
        {
            List<int> nodeChain = new List<int>();
            Queue<int> nodesToTraverse = new Queue<int>();
            HashSet<int> visitedNodes = new HashSet<int>();
            
            nodesToTraverse.Enqueue(nodeIndex);
            
            while (nodesToTraverse.Count > 0)
            {
                
                int targetNode = nodesToTraverse.Dequeue();
                
                visitedNodes.Add(targetNode);
                
                if(!condition.Invoke(targetNode))
                    continue;
                
                nodeChain.Add(targetNode);
                
                
                List<int> adjacentNodes = GetAdjacentNodes(targetNode);
                int adjacentNodeCount = adjacentNodes.Count; 
                for (int i = 0; i < adjacentNodeCount; i++)
                {
                    if(!visitedNodes.Contains(adjacentNodes[i]))
                        nodesToTraverse.Enqueue(adjacentNodes[i]);
                }
            }

            return nodeChain;
        }
    }
}