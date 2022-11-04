using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Donut.BattleSystem
{
    public class CheatMenu : CheatManager
    {
        [SerializeField] private GameObject _cheatMenu;
        [SerializeField] private GameObject _openCheatMenu;

        public void OpenCheatMenu()
        {
            _cheatMenu.SetActive(true);
            _openCheatMenu.SetActive(false);
        }

        public void CloseCheatMenu()
        {
            _cheatMenu.SetActive(false);
            _openCheatMenu.SetActive(true);
        }

        public void ResetPlayers()
        {
            //if(_battleSystem.State != State.)
            foreach(FighterData fighterData in _battleSystem.ListPlayersData)
            {
                fighterData.Fighter.ResetHealth();
            }
        }

        public void ResetEnemies()
        {
            //if(_battleSystem.State != State.)
            foreach (FighterData fighterData in _battleSystem.ListEnemiesData)
            {
                fighterData.Fighter.ResetHealth();
            }
        }

        public void SetGodMode(bool result)
        {
            Debug.Log(result);

            if(result)
            {
                Debug.Log("ToggleOn");
                //_invincibleToggle.isOn = true;
                //_oneShotToggle.isOn = true;
                foreach (FighterData fighterData in _battleSystem.ListPlayersData)
                {
                    Debug.Log("UpdateFighter");
                    fighterData.Fighter.ResetHealth();
                }
            }
            else
            {
                Debug.Log("ToggleOff");
                //_invincibleToggle.isOn = false;
                //_oneShotToggle.isOn = false;
            }
               
        }

        public void SetInvincible(bool result)
        {
            Debug.Log(result);
            foreach (FighterData fighterData in _battleSystem.ListPlayersData)
            {
                Debug.Log("Invincible");
                fighterData.Fighter.SetInvincible(result);
            }
        }

        public void CanOneShot(bool result)
        {
            Debug.Log(result);
            Debug.Log("CanOneShot");
            foreach (FighterData fighterData in _battleSystem.ListPlayersData)
            {   
                fighterData.Fighter.SetOneShotEnemies(result);
            }
        }
    }
}
