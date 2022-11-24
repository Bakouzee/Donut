using Com.Donut.BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameController : MonoBehaviour
{
    [SerializeField] private FighterDatabase player;

    private void Awake()
    {
        SaveGameSystem.InitSaveData();
    }
    public string NameInput { get; set; }

    public void OnSave()
    {
        SaveGameSystem.SaveGame(NameInput, player);
    }

    public void OnLoad()
    {
        SaveGameData.GameData gameData = SaveGameSystem.LoadGameData();
    }
}
