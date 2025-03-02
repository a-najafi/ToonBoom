using System;
using System.Collections.Generic;
using DG.Tweening;
using ToonBoomCore.Gameplay.Entities.PrefabVisualizer;
using ToonBoomCore.Gameplay.Systems;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.State;
using UnityEngine;

namespace ToonBoomCore.MonoBehaviour.EntityVisualizer
{
    public class EntityVisualizerManager : UnityEngine.MonoBehaviour
    {
        [SerializeField]
        private int cellSize = 1;

        private ILevelState levelState;
        
        Dictionary<string, GameObject> prefabVisualizers = new Dictionary<string, GameObject>();
        Dictionary<string, Queue<GameObject>> availableVisualizers = new Dictionary<string, Queue<GameObject>>();
        Dictionary<IGridNodeEntity, GameObject> activeVisualizers = new Dictionary<IGridNodeEntity, GameObject>();

        public int GetNodeIndexFromPosition(Vector3 point)
        {
            IGridState gridState = levelState.GetGridState();
            //offset
            point -= transform.position;
            
            //calc
            int x = Mathf.RoundToInt(point.x / cellSize);
            int y = Mathf.RoundToInt(point.y / cellSize);
            
            //clamp
            x = Mathf.Clamp(x, 0, gridState.GetBoundsWidth() - 1);
            y = Mathf.Clamp(y, 0, gridState.GetBoundsHeight() - 1);
            
            return x + (y * gridState.GetBoundsWidth()) ;
        }

        public Vector3 GetWorldPosition(int index)
        {
            int boundsX = levelState.GetGridState().GetBoundsWidth();
            int x = index % boundsX;
            int y = index / boundsX;
            return new Vector3(x * cellSize,  y * cellSize, 0) + transform.position;
        }
        

        private void OnEntityMoved(IGridNodeEntity entity, int from, int to)
        {
            Vector3 fromPosition = GetWorldPosition(from);
            Vector3 toPosition = GetWorldPosition(to);
            float duration = 0.1f * (fromPosition.y - toPosition.y);
            if(!activeVisualizers.ContainsKey(entity))
                return;
            activeVisualizers[entity].transform.DOMoveY( toPosition.y, duration);
        }

        private GameObject GetVisualizerPrefab(string prefabPath)
        {
            if(prefabVisualizers.ContainsKey(prefabPath))
                return prefabVisualizers[prefabPath];
            
            
            GameObject prefabVisualizer = Resources.Load(prefabPath) as GameObject;
            return prefabVisualizer;
        }

        private GameObject GetVisualizerInstance(string prefabPath)
        {
            if(availableVisualizers.ContainsKey(prefabPath) && availableVisualizers[prefabPath].Count > 0)
                return availableVisualizers[prefabPath].Dequeue();
            
            return Instantiate(GetVisualizerPrefab(prefabPath));
            
        }

        private void OnEntityAdded(IGridNodeEntity gridNodeEntity, int targetNode)
        {
            if (gridNodeEntity is IPrefabVisualizer)
            {
                string path = (gridNodeEntity as IPrefabVisualizer).PrefabVisualizerPath;
                GameObject prefabVisualizer = GetVisualizerInstance(path);
                
                prefabVisualizer.SetActive(true);
                
                activeVisualizers.Add(gridNodeEntity, prefabVisualizer);
                prefabVisualizer.transform.position = GetWorldPosition(targetNode);
            }
            return;
        }
        
        
        private void OnEntityRemoved(IGridNodeEntity gridNodeEntity, int targetNode)
        {
            if (gridNodeEntity is IPrefabVisualizer)
            {
                GameObject visualizerInstance = activeVisualizers[gridNodeEntity];
                
                activeVisualizers.Remove(gridNodeEntity as IPrefabVisualizer);
                
                string path = (gridNodeEntity as IPrefabVisualizer).PrefabVisualizerPath;
                if(!availableVisualizers.ContainsKey(path))
                    availableVisualizers.Add(path, new Queue<GameObject>());
                
                availableVisualizers[path].Enqueue(visualizerInstance);
                visualizerInstance.SetActive(false);
                
            }
            return;
        }

        public void Initialize(ILevelState levelState)
        {
            this.levelState = levelState;
            
            CoreSystemReferenceHandler.Instance.EntityOnGridSystem.OnEntityAdded.AddListener(OnEntityAdded);
            CoreSystemReferenceHandler.Instance.EntityOnGridSystem.OnEntityRemoved.AddListener(OnEntityRemoved);
            CoreSystemReferenceHandler.Instance.MoveSystem.OnEntityMoveOnGrid.AddListener(OnEntityMoved);
        }
    }
}