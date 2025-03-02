using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToonBoomCore.Grid.Basic
{
    [Serializable]
    public class BasicGridNode : IGridNode
    {
        [SerializeField]
        private int index = -1;

        
        [SerializeField] private List<IGridNodeEntity> _entities = null;
       
        
        public BasicGridNode(int index, List<IGridNodeEntity> entities = null)
        {
            if (entities != null)
                this._entities = entities;
            this.index = index;
        }

        public int GetIndex()
        {
            return index;
        }

      

        public List<IGridNodeEntity> GetEntities()
        {
            return _entities;
        }

        public void AddEntity(IGridNodeEntity entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(IGridNodeEntity entity)
        {
            _entities.Remove(entity);
        }
    }
}