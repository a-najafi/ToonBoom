using System;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EntityVisualizer;
using UnityEngine;

namespace ToonBoomCore.GameManager
{
    public class ToonBoomGameManager : UnityEngine.MonoBehaviour
    {
        [SerializeField]
        private SO_LevelDesign _levelDesign;
        
        [SerializeField]
        private EntityVisualizerManager _entityVisualizer;
        
        private IGridState gridState;
        
        private ILevelState levelState;
        
        public ILevelState LevelState => levelState;

        private void Awake()
        {
            
        }

        private void Start()
        {
            levelState = new LevelState(_levelDesign);
            CoreSystemReferenceHandler.Instance.Initialize(levelState);
            _entityVisualizer.Initialize(levelState);
            CoreSystemReferenceHandler.Instance.StartGameSystem.StartGame(ref levelState);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Detect left mouse click
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    int tappedNode = _entityVisualizer.GetNodeIndexFromPosition(hit.point);
                    CoreSystemReferenceHandler.Instance.TapSystem.TapNode(levelState,tappedNode);
                    //Debug.Log($"Clicked on sprite at world position: {mousePosition}");
                }
            }
        }
    }
}