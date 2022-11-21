using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Com.Donut.BattleSystem
{
    public class CheatManager : MonoBehaviour
    {
        private BattleSystem _battleSystem;

        public void Initialize(BattleSystem battleSystem)
        {
            _battleSystem = battleSystem;
        }
        
        public enum CheatReceiver
        {
            Player1,
            Player2,
            AllPlayers,
            AllEnemies,
            All,
        }

        [Tooltip("Fighter that receive cheat")] public CheatReceiver cheatReceiverState;

        //Use delegate to store functions
        delegate void MyDelegate(List<FighterData> _listFighterData);
        MyDelegate myDelegate;

        public void ApplyCheats()
        {
        }

        public List<FighterData> FindReceiver()
        {
            var listFighterData = new List<FighterData>();
            switch(cheatReceiverState)
            {
                case CheatReceiver.Player1:
                    if(_battleSystem != null)
                        listFighterData.Add(_battleSystem.ListPlayersData[0]);
                    Debug.Log(_battleSystem);
                    break;
                case CheatReceiver.Player2:
                    listFighterData.Add(_battleSystem.ListPlayersData[1]);
                    break;
                case CheatReceiver.AllPlayers:
                    listFighterData = _battleSystem.ListPlayersData;
                    break;
                case CheatReceiver.AllEnemies:
                    listFighterData = _battleSystem.ListEnemiesData;
                    break;
                case CheatReceiver.All:
                    listFighterData = _battleSystem.ListEnemiesData;
                    listFighterData.AddRange(_battleSystem.ListPlayersData);
                    break;
            }

            return listFighterData;
        }

        public void AddResetHealth() { myDelegate += ResetHealth; }

        public void AddSetInvincible() { myDelegate += SetInvincible; }
        public void AddCanOneShot() { 
            myDelegate = CanOneShot;

            if(myDelegate != null)
                myDelegate.Invoke(FindReceiver());
        }    
        public void AddSetGodMode() { myDelegate += SetInvincible; myDelegate += CanOneShot; myDelegate += ResetHealth; }

        #region FunctionsCheat
        private void ResetHealth(List<FighterData> _listFighterData)
        {
            foreach (FighterData fighterData in _listFighterData)
            {
                fighterData.Fighter.ResetHealth();
            }
        }

        private void SetInvincible(List<FighterData> _listFighterData)
        {
            foreach (FighterData fighterData in _listFighterData)
            {
                fighterData.Fighter.SetInvincible(true);
            }
        }

        private void CanOneShot(List<FighterData> _listFighterData)
        {
            foreach (FighterData fighterData in _listFighterData)
            {
                fighterData.Fighter.SetOneShotEnemies(!fighterData.Fighter.CanOneShot);
            }
        }
        #endregion FunctionsCheat
    }
}