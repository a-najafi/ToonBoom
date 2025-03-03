using System;
using ToonBoomCore.Player.Design;
using ToonBoomCore.Player.State;
using ToonBoomCore.Player.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ToonBoomCore.MonoBehaviour.MainMenu
{
    public class MainMenu : UnityEngine.MonoBehaviour
    {
        private IPlayerDesign playerDesign = null;
        
        [SerializeField]private Text _levelNumberText;
        
        [SerializeField]private Button _nextLevelButton;
        [SerializeField]private Button _lastLevelButton;
        [SerializeField]private Button _startGameButton;


        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(GoToNextLevel);
            _lastLevelButton.onClick.AddListener(GoToLastLevel);
            _startGameButton.onClick.AddListener(StartGame);
        }

        private void Start()
        {
            playerDesign = PlayerSystem.Instance.LoadPlayerDesign();
            
            UpdateBasedOnPlayerState();
            
        }

        protected void UpdateBasedOnPlayerState()
        {
            IPlayerState playerState = PlayerSystem.Instance.LoadPlayerState();
            int currentLevel = playerState.CurrentLevel;
            
            _lastLevelButton.interactable = currentLevel != 0;
            _nextLevelButton.interactable = currentLevel < playerDesign.LevelsToPlay.Count && playerState.CompletedLevels.Contains(currentLevel);
            _levelNumberText.text = currentLevel.ToString();
        }

        public void GoToNextLevel()
        {
            IPlayerState playerState = PlayerSystem.Instance.LoadPlayerState();
            
            int currentLevel = playerState.CurrentLevel;
            PlayerSystem.Instance.SetPlayerCurrenLevel(currentLevel + 1);
            UpdateBasedOnPlayerState();
            
        }

        public void GoToLastLevel()
        {
            IPlayerState playerState = PlayerSystem.Instance.LoadPlayerState();
            
            int currentLevel = playerState.CurrentLevel;
            PlayerSystem.Instance.SetPlayerCurrenLevel(currentLevel - 1);
            UpdateBasedOnPlayerState();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("GameLevel");
        }
        
        
    }
}