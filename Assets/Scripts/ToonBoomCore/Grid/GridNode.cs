using System.Collections.Generic;

namespace ToonBoomCore.Grid
{
    public interface IGridNode
    {
        
        int GetIndex();

        
        List<IGridNodeEntity> GetEntities();
        
        void AddEntity(IGridNodeEntity entity);
        
        void RemoveEntity(IGridNodeEntity entity);
    }
    
}