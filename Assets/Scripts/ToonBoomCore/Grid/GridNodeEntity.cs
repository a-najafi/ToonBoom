using System;
using UnityEngine;

namespace ToonBoomCore.Grid
{
    
    // different entities which are placed on the board react to three types of actions
    // 1- when they are tapped directly from user input
    // 2- when they are cleared from effects from other entities
    // 3- when an adjacent node is cleared
    public interface IGridNodeEntity
    {
        
        int GetIndex();
        
        void SetIndex(int value);
        
        IGridNodeEntity GetCopy();
        
        void CopyTo(IGridNodeEntity other);
    }
    
    

    [Serializable]
    public abstract class GridNodeEntityBase : IGridNodeEntity
    {
        [SerializeField]
        private int index = -1;

        public GridNodeEntityBase()
        {
            this.index = -1;
        }
        public GridNodeEntityBase(int index)
        {
            this.index = index;
        }
        
        public int GetIndex()
        {
            return index;
        }

        public void SetIndex(int value)
        {
            index = value;
        }

        public virtual IGridNodeEntity GetCopy()
        {
            throw new System.NotImplementedException();
        }

        public virtual void CopyTo(IGridNodeEntity other)
        {
            throw new System.NotImplementedException();
        }
    }
    
    [Serializable]
    public class GridNodeEntity : GridNodeEntityBase
    {
        public GridNodeEntity()
        {
            
        }
        public GridNodeEntity(int index) : base(index)
        {
        }
    }
    
}