using System;
using System.Linq;
using ToonBoomCore.Gameplay.Systems.Core;
using ToonBoomCore.Grid;
using ToonBoomCore.Level.Design;
using ToonBoomCore.Level.State;
using ToonBoomCore.MonoBehaviour.EntityVisualizer;
using ToonBoomCore.MonoBehaviour.EventSequencer;
using ToonBoomCore.Player.Design;
using ToonBoomCore.Player.State;
using ToonBoomCore.Player.Systems;
using UnityEngine;

namespace ToonBoomCore.GameManager
{
    public class ToonBoomGameManager : UnityEngine.MonoBehaviour
    {
        
        private static ToonBoomGameManager _instance;
        
        public static ToonBoomGameManager Instance => _instance ??= FindObjectOfType<ToonBoomGameManager>();


        
        
        [SerializeField]
        private EntityVisualizerManager _entityVisualizer;
        
        [SerializeField]
        private EventSequencer _eventSequencer;
        
        
        
        private IGridState gridState;
        
        private ILevelState levelState;
        
        public ILevelState LevelState => levelState;
        
        public EntityVisualizerManager EntityVisualizer => _entityVisualizer;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            IPlayerState playerState = PlayerSystem.Instance.LoadPlayerState();
            IPlayerDesign playerDesign = PlayerSystem.Instance.LoadPlayerDesign();
            
            if(playerState.CurrentLevel >= playerDesign.LevelsToPlay.Count())
                throw new System.Exception("Level " + playerState.CurrentLevel + " is out of range");
            
            levelState = new LevelState(playerDesign.LevelsToPlay[playerState.CurrentLevel]);
            
            CoreSystemReferenceHandler.Instance.Initialize(levelState);
            _entityVisualizer.Initialize(levelState);
            _eventSequencer.Initialize(levelState);
            CoreSystemReferenceHandler.Instance.StartGameSystem.StartGame(ref levelState);
        }

        private void Update()
        {
            // do not respond to input if event sequencer is processing
            if(_eventSequencer.IsProcessing)
                return;
            
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