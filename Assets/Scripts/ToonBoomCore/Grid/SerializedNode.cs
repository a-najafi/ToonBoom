using System;
using System.Collections.Generic;
using ToonBoomCore.Utility;
using UnityEngine;

namespace ToonBoomCore.Grid
{
    [Serializable]
    public class SerializedNode : IGridNode
    {
        [SerializeField, ReadOnly]
        private int index; 
        
        [SerializeReference]
        private List<GridNodeEntityBase> _entities = new List<GridNodeEntityBase>();
        
        private List<IGridNodeEntity> entities = null;

        public SerializedNode(int index)
        {
            this.index = index;
            _entities.Clear();
        }
        
        public int GetIndex()
        {
            return index;
        }

        public List<IGridNodeEntity> GetEntities()
        {
            return entities ??=  _entities.ConvertAll(entity => entity as IGridNodeEntity);
        }
        
        public List<GridNodeEntityBase> SerializedEntities => _entities;

        
        public void AddEntity(IGridNodeEntity entity)
        {
            _entities.Add(entity as GridNodeEntityBase);
            entities = _entities.ConvertAll(entity => entity as IGridNodeEntity);
        }

        public void RemoveEntity(IGridNodeEntity entity)
        {
            _entities.Remove(entity as GridNodeEntityBase);
            entities = _entities.ConvertAll(entity => entity as IGridNodeEntity);
        }
    }
}