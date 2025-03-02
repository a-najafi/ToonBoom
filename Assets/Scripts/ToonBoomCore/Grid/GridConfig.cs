using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToonBoomCore.Grid
{
    // provides grid configuration to be used to setup initial grid state
    // inheriting classes should be serializable
    public interface IGridConfig
    {
        //bounds define to boundry of the world not the actual tiles that are displayed
        int GetBoundsWidth();
        int GetBoundsHeight();
        
         List<IGridNode> Nodes { get; }

        // the configuration class should provide a valid usable grid state for initialization purposes 
        IGridState GetNewGridState();

        // the configuration class should be able to modify existing grid state to match initial state
        void ApplyConfig(ref IGridState gridState);
        
    }

    [Serializable]
    public abstract class GridConfigBase : IGridConfig
    {
        
        public virtual int GetBoundsWidth()
        {
            throw new NotImplementedException();
        }

        public virtual int GetBoundsHeight()
        {
            throw new NotImplementedException();
        }

        public virtual List<IGridNode> Nodes => null;

        public virtual IGridState GetNewGridState()
        {
            throw new NotImplementedException();
        }

        public virtual void ApplyConfig(ref IGridState gridState)
        {
            throw new NotImplementedException();
        }
    }

   
}
