using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveGameData;

public static class SaveGameSystem
{
    public static void InitSaveData()
    {
        string path = Application.persistentDataPath + "/Game.save";

        if (File.Exists(path))
            return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, new SaveData());
        stream.Close();

        Debug.Log("Init save successful");
    }

    #region GAME
    public static void SaveGame(string name, FighterDatabase player)
    {
        AudioMixer aM = AudioManager.Instance.MainAudioSource.outputAudioMixerGroup.audioMixer;
        aM.GetFloat("MasterVolume", out float masterValue);
        aM.GetFloat("MusicVolume", out float musicValue);
        aM.GetFloat("SFXVolume", out float sfxValue);
        SaveGameData.GameData saveGame = new SaveGameData.GameData(SceneManager.GetActiveScene().name, name, masterValue, musicValue, sfxValue,
            new SaveGameData.PlayerData(player.PlayersList[0].CurrentHealth));

        SaveGameData data = new SaveGameData(saveGame);

        string path = Application.persistentDataPath + "/Game.save";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save game successful");
    }

    public static SaveGameData.GameData LoadGameData(Fighter player)
    {
        string path = Application.persistentDataPath + "/Game.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveGameData data = formatter.Deserialize(stream) as SaveGameData;

            AudioMixer aM = AudioManager.Instance.MainAudioSource.outputAudioMixerGroup.audioMixer;
            aM.SetFloat("MasterVolume", data.MyGameData.masterVolume);
            aM.SetFloat("MusicVolume", data.MyGameData.musicVolume);
            aM.SetFloat("SFXVolume", data.MyGameData.sfxVolume);
            player.CurrentHealth = data.MyGameData.Player.life;
            SceneManager.LoadScene(data.MyGameData.sceneName);

            RestoreValueSliders(data.MyGameData.masterVolume, data.MyGameData.musicVolume, data.MyGameData.sfxVolume);
            stream.Close();

            Debug.Log("Load game successful");

            return data.MyGameData;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            ResetGameData();
            return LoadGameData(player);
        }
    }

    private static void RestoreValueSliders(float masterVolume, float musicVolume, float sfxVolume)
    {
        Transform menuUI = GameObject.FindGameObjectWithTag("MenuUI").transform;
        menuUI.GetChild(0).GetComponent<Slider>().value = masterVolume;
        menuUI.GetChild(1).GetComponent<Slider>().value = musicVolume;
        menuUI.GetChild(2).GetComponent<Slider>().value = sfxVolume;
    }

    public static void ResetGameData()
    {
        SaveGameData data = new SaveGameData(new SaveGameData.GameData());

        string path = Application.persistentDataPath + "/Game.save";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Reset game successful");
    }

    #endregion
}
