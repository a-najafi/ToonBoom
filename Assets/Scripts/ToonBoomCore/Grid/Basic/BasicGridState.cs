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
            _gridNodes = new List<IGridNode>(nodeNum);
            for (int i = 0; i < nodeNum; i++)
            {
                _gridNodes.Add(new BasicGridNode(i,new List<IGridNodeEntity>()));
                // for (int j = 0; j < nodeEntities[i].Count; j++)
                // {
                //     _gridNodes[i].AddEntity(nodeEntities[i][j].GetCopy());
                // }
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
            return IsValidNodeIndex(GetIndexFromLocation(x, y));
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
            
            
            
            if(IsValidNodeIndex(index + 1) && IsValidX( x + 1))
                adjacentNodes.Add(index + 1);
            if(IsValidNodeIndex(index - 1) && IsValidX(x - 1))
                adjacentNodes.Add(index - 1);
            if(IsValidNodeIndex(index + boundsX ) && IsValidY( y + 1))
                adjacentNodes.Add(index + boundsX);
            if(IsValidNodeIndex(index - boundsX) && IsValidY( y - 1))
                adjacentNodes.Add(index - boundsX);

            if (includeDiagonal)
            {
                if(IsValidNodeIndex(index + 1 + boundsX) && IsValidX( x + 1) && IsValidY( y + 1))
                    adjacentNodes.Add(index + 1 + boundsX);
                if(IsValidNodeIndex(index - 1 + boundsX) && IsValidX( x - 1)&& IsValidY( y - 1))
                    adjacentNodes.Add(index - 1 + boundsX);
                if(IsValidNodeIndex(index + 1 - boundsX) && IsValidX( x + 1)&& IsValidY( y + 1))
                    adjacentNodes.Add(index + 1 - boundsX);
                if(IsValidNodeIndex(index - 1 - boundsX) && IsValidX( x - 1)&& IsValidY( y - 1))
                    adjacentNodes.Add(index - 1 - boundsX);
            }

            return adjacentNodes;


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