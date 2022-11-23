using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private void Awake()
    {
        SaveSystem.InitSaveData();
    }

    public string NameInput { get; set; }
    public Text txtGameName;

    public void OnSave()
    {
        SaveSystem.SaveGameData(NameInput);
    }

    public void OnLoad()
    {
        SaveData.GameData gameData = SaveSystem.LoadGameData();
        txtGameName.text = gameData.gameName;
    }
}
