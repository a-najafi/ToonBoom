using System;
using System.IO;
using ToonBoomCore.Player.Design;
using ToonBoomCore.Player.State;
using UnityEngine;

namespace ToonBoomCore.Player.Systems
{
    public class PlayerSystem
    {

        private static string savePath = Application.persistentDataPath + "/savefile.json";

        private static PlayerSystem _instance;

        public static PlayerSystem Instance => _instance ?? (_instance = new PlayerSystem());

        public IPlayerDesign LoadPlayerDesign()
        {
            return  Resources.Load<SO_PlayerDesign>("PlayerDesign");
        }

        public PlayerState LoadPlayerState()
        {
            
            if (!File.Exists(savePath))
            {
                Debug.LogWarning("Save file not found: " + savePath);
                PlayerState playerState = new PlayerState();
                SavePlayerState(playerState);
                return playerState;
            }
            
            try
            {
                string json = File.ReadAllText(savePath);
                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("Save file is empty.");
                    return null;
                }

                PlayerState loadedData = JsonUtility.FromJson<PlayerState>(json);
                if (loadedData == null)
                {
                    Debug.LogError("Failed to deserialize save file.");
                }

                return loadedData;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading save file: " + ex.Message);
                return null;
            }
        

        }

        public void CompleteLevel(int level)
        {
            PlayerState playerState = LoadPlayerState();
            if (!playerState.CompletedLevels.Contains(level))
            {
                playerState.CompleteLevel(level);
                SavePlayerState(playerState);
            }
            
        }

        public void SetPlayerCurrenLevel(int level)
        {
            PlayerState playerState = LoadPlayerState();
            IPlayerDesign playerDesign = LoadPlayerDesign();
            playerState.SetCurrentLevel(Mathf.Clamp(level, 0, playerDesign.LevelsToPlay.Count - 1));
            SavePlayerState(playerState);
        }

        public void SavePlayerState(PlayerState playerState)
        {
            string json = JsonUtility.ToJson(playerState, true);
            File.WriteAllText(savePath, json);
            Debug.Log("Saved to " + savePath);
        }


    }


}