using System;
using ToonBoomCore.Gameplay.Entities.PrefabVisualizer;
using ToonBoomCore.Grid;
using ToonBoomCore.MonoBehaviour.EntityVisualizer;
using UnityEditor;
using UnityEngine;

namespace ToonBoomEditor.Grid
{
    [ExecuteInEditMode]
    public class GridNodeEntityEditor : MonoBehaviour
    {
        [SerializeField] private EntityVisualizer entityVisualizerInstance;
        [SerializeReference]private GridNodeEntityBase gridNodeEntity;

        public GridNodeEntityBase GridNodeEntity => gridNodeEntity;

        private void OnEnable()
        {
            
        }

        public void Initialize(GridNodeEntityBase gridNodeEntity)
        {
            this.gridNodeEntity = gridNodeEntity;
            OnValidate();
        }

        private void DelayedDestroy(GameObject gameObject)
        {
            EditorApplication.delayCall += () =>
            {
                if (this != null) // Check if still exists before destroying
                {
                    DestroyImmediate(gameObject);
                    Debug.Log("GameObject destroyed in Edit Mode.");
                    if (gridNodeEntity != null)
                    {
                        
                        Debug.Log("Path to load : " + (gridNodeEntity as IPrefabVisualizer).PrefabVisualizerPath);
                        
                        EntityVisualizer entityVisualizerPrefab =
                            Resources.Load<EntityVisualizer>((gridNodeEntity as IPrefabVisualizer).PrefabVisualizerPath);
                        
                        entityVisualizerInstance = Instantiate<EntityVisualizer>(entityVisualizerPrefab, transform);
                    
                        entityVisualizerInstance.transform.localScale = Vector3.one;
                        entityVisualizerInstance.Initialize(gridNodeEntity);
                    }
                }
            };
        }

        private void OnValidate()
        {
            
            if (gridNodeEntity == null && entityVisualizerInstance != null)
            {
                DelayedDestroy(entityVisualizerInstance.gameObject);
                return;
            }

            if (gridNodeEntity != null && entityVisualizerInstance != null && 
                (entityVisualizerInstance.GetBoundToEntity == null || 
                 gridNodeEntity.GetType() !=  entityVisualizerInstance.GetBoundToEntity.GetType()))
            {
                DelayedDestroy(entityVisualizerInstance.gameObject);
            }
           
            
            if(gridNodeEntity == null)
                return;
            
            if (entityVisualizerInstance == null)
            {
                Debug.Log("entity is not null : " + gridNodeEntity.GetType().Name );
                Debug.Log("Path to load : " + (gridNodeEntity as IPrefabVisualizer).PrefabVisualizerPath);

                EntityVisualizer entityVisualizerPrefab =
                    Resources.Load<EntityVisualizer>((gridNodeEntity as IPrefabVisualizer).PrefabVisualizerPath);
                entityVisualizerInstance = Instantiate<EntityVisualizer>(entityVisualizerPrefab, transform);
            }
            entityVisualizerInstance.transform.localScale = Vector3.one;
            entityVisualizerInstance.Initialize(gridNodeEntity);
            
        }

        private void Update()
        {
            if(entityVisualizerInstance != null)
                entityVisualizerInstance.transform.localScale = Vector3.one;
            transform.position = GridConfigEditor.Instance.GetSnapPosition(transform.position);
            gridNodeEntity.SetIndex(GridConfigEditor.Instance.GetGridIndexFromPoint(transform.position));
            
        }
    }
}