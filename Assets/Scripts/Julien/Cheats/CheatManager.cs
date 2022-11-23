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

        public bool IsInBattle;
        public bool IsInitialized;
        public void Initialize(BattleSystem battleSystem)
        {
            _battleSystem = battleSystem;
            IsInitialized = true;
        }
        
        public enum CheatReceiver
        {
            Player1,
            Player2,
            AllEnemies,
        }

        [Tooltip("Fighter that receive cheat")] public CheatReceiver cheatReceiverState;

        //Use delegate to store functions
        delegate void MyDelegate(List<FighterData> _listFighterData);
        MyDelegate myDelegate;

        public List<FighterData> FindReceiver()
        {
            var listFighterData = new List<FighterData>();
            switch(cheatReceiverState)
            {
                case CheatReceiver.Player1:
                    listFighterData.Add(_battleSystem.ListPlayersData[0]);
                    break;
                case CheatReceiver.Player2:
                    listFighterData.Add(_battleSystem.ListPlayersData[1]);
                    break;
                case CheatReceiver.AllEnemies:
                    listFighterData = _battleSystem.ListEnemiesData;
                    break;
            }

            return listFighterData;
        }

        public void AddSetFullHealth()
        {
            myDelegate = SetFullHealth;
            if(myDelegate != null)
                myDelegate.Invoke(FindReceiver());
        }

        public void AddSetInvincible() 
        {
            myDelegate = SetInvincible;
            if(myDelegate != null)
                myDelegate.Invoke(FindReceiver());
            
        }
        public void AddCanOneShot() { 
            myDelegate = CanOneShot;
            if(myDelegate != null)
                myDelegate.Invoke(FindReceiver());
        }

        public void AddSetGodMode()
        {
            myDelegate = SetInvincible; 
            myDelegate += CanOneShot; 
            myDelegate += SetFullHealth;
            if(myDelegate != null)
                myDelegate.Invoke(FindReceiver());
        }

        #region FunctionsCheat
        private void SetFullHealth(List<FighterData> _listFighterData)
        {
            foreach (FighterData fighterData in _listFighterData)
            {
                fighterData.Fighter.ResetHealth(!fighterData.Fighter.IsFullHealth);
            }
        }

        private void SetInvincible(List<FighterData> _listFighterData)
        {
            foreach (FighterData fighterData in _listFighterData)
            {
                fighterData.Fighter.SetInvincible(!fighterData.Fighter.IsInvincible);
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