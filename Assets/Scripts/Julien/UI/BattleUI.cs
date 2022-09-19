﻿using System;
 using UnityEditor.Animations;
 using UnityEngine;
using UnityEngine.UI;

namespace Com.Donut.BattleSystem
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private GameObject fighterPrefab;
        [SerializeField] private Transform playerParent0;
        [SerializeField] private Transform playerParent1;
        [SerializeField] private Transform enemyParent;
        [SerializeField] private Nameplate playerNameplate0;
        [SerializeField] private Nameplate playerNameplate1;
        [SerializeField] private NameplateBoss enemyNameplate0;
        [SerializeField] private InputControllerUI inputControllerUI;
        [SerializeField] private ActionController actionController;
        [SerializeField] private Text dialogText;
        [SerializeField] private GameObject PauseScreen;

        public void Initialize(Fighter player0, Fighter player1, Fighter enemy, Sprite sprite)
        {
            InitializePlayer(player0, player1);
            InitializeEnemy(enemy);
            InitializeBattleField(sprite);
            
            actionController.InitializeActionUI(player0.Abilities); //Uniquement avec les attaque du joueur 0
        }

        public void UpdateUI()
        {
            playerNameplate0.UpdateNameplate();
            playerNameplate1.UpdateNameplate();
        }
        
        //public void InputUI()

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowPauseMenu();
            }
        }

        /*public void SetDialogText(string text)
        {
            dialogText.text = text;
        }*/

        public void ShowPauseMenu()
        {
            PauseScreen.SetActive(true);
        }
        
        public void HidePauseMenu()
        {
            PauseScreen.SetActive(false);
        }

        private void InitializePlayer(Fighter fighter0, Fighter fighter1)
        {
            playerNameplate0.Initialize(fighter0);
            playerNameplate1.Initialize(fighter1);

            var go0 = Instantiate(fighterPrefab, playerParent0, false);
            var go1 = Instantiate(fighterPrefab, playerParent1, false);
            var image0 = go0.GetComponent<Image>();
            image0.sprite = fighter0.Sprite;
            var anim0 = image0.GetComponent<Animator>();
            anim0.runtimeAnimatorController = fighter0.AnimatorController;
            
            var image1 = go1.GetComponent<Image>();
            image1.sprite = fighter1.Sprite;
            var anim1 = image1.GetComponent<Animator>();
            anim1.runtimeAnimatorController = fighter1.AnimatorController;
        }
        
        private void InitializeEnemy(Fighter fighter)
        {
            enemyNameplate0.Initialize(fighter);
                
            var go = Instantiate(fighterPrefab, enemyParent, false);
            go.transform.localScale = Vector3.one * 0.8f;
            var image = go.GetComponent<Image>();
            image.sprite = fighter.Sprite;
        }

        private void InitializeBattleField(Sprite sprite)
        {
            background.sprite = sprite;
        }

        public void ShowAction()
        {
            actionController.ShowActionUI();
            BattleSystem.CanUseInput = true;
        }
        
        public void HideAction()
        {
            actionController.HideActionUI();
            BattleSystem.CanUseInput = false;
        }

        public void ShowInputPlayer0()
        {
            inputControllerUI.ShowInputPlayer0();
        }
        
        public void ShowInputPlayer1()
        {
            inputControllerUI.ShowInputPlayer1();
        }

        public void HideInputPlayers()
        {
            inputControllerUI.HideInputPlayers();
        }

        public void ShiftAction()
        {
            actionController.UpdateUI();
        }
    }
}
