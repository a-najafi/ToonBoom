using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ToonBoomCore.MonoBehaviour.EndGameScreen
{
    public class EndGameScreen : UnityEngine.MonoBehaviour
    {
        [SerializeField]private GameObject screen;
        [SerializeField]private Text endGameText;
        [SerializeField]private Button endGameButton;

        private void Awake()
        {
            endGameButton.onClick.AddListener(OnEndGameScreenClicked);
        }

        void OnEndGameScreenClicked()
        {
            DOTween.KillAll();
            SceneManager.LoadScene("MainMenu");
        }

        public void Show(bool show, string message)
        {
            FindObjectOfType<StandaloneInputModule>().enabled = show;
            screen.SetActive(show);
            endGameText.text = message;
            
        }
        
    }
}