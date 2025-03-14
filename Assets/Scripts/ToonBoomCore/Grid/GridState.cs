using System;
using System.Collections.Generic;

namespace ToonBoomCore.Grid
{
    // in a 2D game like toon blast nodes are also tiles but treating them as nodes allows for modularity
    // a grid is a collection of Nodes.
    // each node can contain multiple entities
    // grid state is used to track and store relevant data for different nodes in the game world.
    // the game world is made up of a series of nodes where different entities can be placed
    
    
    
    public interface IGridState
    {

        int GetNodeNum();
        int GetBoundsWidth();
        int GetBoundsHeight();

        bool IsValidNodeIndex(int index);

        bool IsValidNodeLocation(int x, int y);
        
        List<IGridNode> GetNodes();

        IGridNode GetNodeAt(int index);
        
        IGridNode GetNodeAt(int x, int y);

        List<int> GetAdjacentNodes(int index,bool includeDiagonal = false);
        
        List<int> GetAdjacentNodes(int x, int y,bool includeDiagonal = false);

        public List<int> GetNodeChain(int nodeIndex, Func<int, bool> condition);

        public List<int> GetSquareAOE(int nodeIndex, int range);

    }
    
}