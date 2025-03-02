using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ToonBoomCore.Grid.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace ToonBoomEditor.Grid
{
    [ExecuteInEditMode]
    public class GridConfigEditor : MonoBehaviour
    {
        
        private static GridConfigEditor _instance;
        
        public static GridConfigEditor Instance => _instance ??= FindObjectOfType<GridConfigEditor>();

        [SerializeField] private float cellSize;
        [SerializeField] private SO_BasicGridConfig gridConfig;
        [SerializeField] private GridNodeEntityEditor entityEditorPrefab;


        private void Awake()
        {
            _instance = this;
        }

        public Vector3 GetSnapPosition(Vector3 position)
        {
            return GetWorldPosition(GetGridIndexFromPoint(position));
        }

        public int GetGridIndexFromPoint(Vector3 point)
        {
            //offset
            point -= transform.position;
            
            //calc
            int x = Mathf.RoundToInt(point.x / cellSize);
            int y = Mathf.RoundToInt(point.y / cellSize);
            
            //clamp
            x = Mathf.Clamp(x, 0, gridConfig.GetBoundsWidth() - 1);
            y = Mathf.Clamp(y, 0, gridConfig.GetBoundsHeight() - 1);
            
            return x + (y * gridConfig.GetBoundsWidth()) ;
        }
        
        public Vector3 GetWorldPosition(int gridIndex)
        {
            float x = ((gridIndex % gridConfig.GetBoundsWidth()) * cellSize) + transform.position.x;
            float y = ((gridIndex / gridConfig.GetBoundsWidth()) * cellSize) + transform.position.y;

            return new Vector3(x, y, 0);

        }
        
        

        [ContextMenu("Save Grid Config")]
        public void SaveToConfig()
        {
            EditorUtility.SetDirty(gridConfig);
            
            for (int i = 0; i < gridConfig.SerializedNodes.Count(); i++)
            {
                gridConfig.SerializedNodes[i].SerializedEntities.Clear();
            }
            
            Debug.Log(" Nodes size : " + gridConfig.SerializedNodes.Count());
            
            
            GridNodeEntityEditor[] NodeEntityEditors = FindObjectsOfType<GridNodeEntityEditor>();
            for (int i = 0; i < NodeEntityEditors.Length; i++)
            {
                int index = GetGridIndexFromPoint(NodeEntityEditors[i].transform.position);
                
                Debug.Log(" index from position : " + NodeEntityEditors[i].transform.position + " is " + index);
                
                gridConfig.SerializedNodes[index].AddEntity(NodeEntityEditors[i].GridNodeEntity);
            }
            
        }
        
        private void DelayedDestroy(GameObject gameObject)
        {
            EditorApplication.delayCall += () =>
            {
                if (this != null) // Check if still exists before destroying
                {
                    DestroyImmediate(gameObject);
                   
                }
            };
        }
        
        [ContextMenu("Load From Config")]
        public void LoadFromConfig()
        {
            
            
            
            Debug.Log(" Nodes size : " + gridConfig.SerializedNodes.Count());
            
            
            GridNodeEntityEditor[] NodeEntityEditors = FindObjectsOfType<GridNodeEntityEditor>();
            for (int i = 0; i < NodeEntityEditors.Length; i++)
            {
                DestroyImmediate(NodeEntityEditors[i].gameObject);
            }

            for (int i = 0; i < gridConfig.SerializedNodes.Count(); i++)
            {
                for (int j = 0; j < gridConfig.SerializedNodes[i].SerializedEntities.Count; j++)
                {
                    GridNodeEntityEditor gridNodeEntityEditor = Instantiate(entityEditorPrefab, transform);
                    gridNodeEntityEditor.transform.position = GetWorldPosition(i);
                    gridNodeEntityEditor.Initialize(gridConfig.SerializedNodes[i].SerializedEntities[j]);
                }
                
            }
            
        }
    }

}