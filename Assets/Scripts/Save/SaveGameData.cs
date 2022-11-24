using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveGameData;

[System.Serializable]
public class SaveGameData
{
    [System.Serializable]
    public struct PlayerData
    {
        public int life;

        public PlayerData(int health)
        {
            life = health;
        }
    }

    [System.Serializable]
    public struct GameData
    {
        public string gameName;
        public PlayerData Player;
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public GameData(string name, float masterV, float musicV, float sfxV, PlayerData player)
        {
            string newName = string.IsNullOrEmpty(name) ? "New Game Name" : name;

            gameName = newName;
            masterVolume = masterV;
            musicVolume = musicV;
            sfxVolume = sfxV;
            this.Player = player;
        }
    }
    public GameData MyGameData { get; private set; }

    public SaveGameData()
    {
        MyGameData = new GameData();
    }

    public SaveGameData(GameData gameData)
    {
        MyGameData = gameData;
    }
}
