﻿using System;
 using UnityEditor.Animations;
 using UnityEngine;
using UnityEngine.UI;

namespace Com.Donut.BattleSystem
{
    public class BattleUI : MonoBehaviour
    {
        private BattleSystem _battleSystem;
        
        [Header("Initialize battle")]
        [SerializeField] private Image background;
        [SerializeField] private GameObject fighterPrefab;
        [SerializeField] private Transform playerParent0;
        [SerializeField] private Transform playerParent1;
        [SerializeField] private Transform enemyParent;
        [SerializeField] private Nameplate playerNameplate0;
        [SerializeField] private Nameplate playerNameplate1;
        [SerializeField] private NameplateBoss enemyNameplate0;
        [SerializeField] private ActionController actionController;
        [SerializeField] private Text dialogText;
        [SerializeField] private GameObject pauseScreen;
        
        //To have ref of players animator
        private Animator _animPlayer0;
        private Animator _animPlayer1;

        [Header("DisplayInput_UI")]
        [SerializeField] private Image input0;
        [SerializeField] private Image input1;
        [SerializeField] private Animator animInput0;
        [SerializeField] private Animator animInput1;
        

        public void Initialize(BattleSystem battleSystem, Fighter player0, Fighter player1, Fighter enemy, Sprite sprite)
        {
            _battleSystem = battleSystem;
            InitializePlayer(player0, player1);
            InitializeEnemy(enemy);
            InitializeBattleField(sprite);
            
            actionController.InitializeActionUI(player0.Abilities, player1.Abilities, _battleSystem);
        }
        
        private void InitializePlayer(Fighter fighter0, Fighter fighter1)
        {
            playerNameplate0.Initialize(fighter0);
            playerNameplate1.Initialize(fighter1);

            _battleSystem.player0Go = Instantiate(fighterPrefab, playerParent0, false);
            var image0 = _battleSystem.player0Go.GetComponent<Image>();
            image0.sprite = fighter0.Sprite;
            _animPlayer0 = image0.GetComponent<Animator>();
            _animPlayer0.runtimeAnimatorController = fighter0.AnimatorController;
            
            _battleSystem.player1Go = Instantiate(fighterPrefab, playerParent1, false);
            var image1 = _battleSystem.player1Go .GetComponent<Image>();
            image1.sprite = fighter1.Sprite;
            _animPlayer1 = image1.GetComponent<Animator>();
            _animPlayer1.runtimeAnimatorController = fighter1.AnimatorController;

            actionController.InitializeAnimator(_animPlayer0, _animPlayer1);
        }
        
        private void InitializeEnemy(Fighter fighter)
        {
            enemyNameplate0.Initialize(fighter);
                
            var go = Instantiate(fighterPrefab, enemyParent, false);
            go.transform.localScale = Vector3.one * 0.8f;
            _battleSystem.PlayerTargetTransform = go.transform;
            var image = go.GetComponent<Image>();
            image.sprite = fighter.Sprite;
            
            var anim = image.GetComponent<Animator>();
            anim.runtimeAnimatorController = fighter.AnimatorController;

        }

        private void InitializeBattleField(Sprite sprite)
        {
            background.sprite = sprite;
        }

        public void UpdateUI()
        {
            playerNameplate0.UpdateNameplate();
            playerNameplate1.UpdateNameplate();
        }

        /*public void SetDialogText(string text)
        {
            dialogText.text = text;
        }*/

        public void ShowPauseMenu()
        {
            pauseScreen.SetActive(true);
        }
        
        public void HidePauseMenu()
        {
            pauseScreen.SetActive(false);
        }
        public void SetActiveAbility(Fighter fighter, bool result)
        {
            actionController.SetActiveAbility_UI(fighter, result);
        }

        public void SetActivePlayerInput(Fighter fighter, bool result)
        {
            if(fighter == _battleSystem.Player0)
                input0.gameObject.SetActive(result);
            else
                input1.gameObject.SetActive(result);
        }

        public Abilities ShiftAction(Fighter fighter)
        {
            return actionController.UpdateCurrentAbility(fighter);
        }

        public void SetAnimTrigger(Fighter fighter, string triggerName)
        {
            if(fighter == _battleSystem.Player0)
                _animPlayer0.SetTrigger(triggerName);
            else
                _animPlayer1.SetTrigger(triggerName);
        }

        public void LaunchAbility(Fighter fighter)
        {
            actionController.LaunchAbility(fighter);
        }

        public void ResetAnimator()
        {
            actionController.ResetAnimator();
        }
    }
}
