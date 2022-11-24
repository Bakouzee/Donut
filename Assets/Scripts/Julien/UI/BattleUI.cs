using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Com.Donut.BattleSystem
{
    public class BattleUI : MonoBehaviour
    {
        private BattleSystem _battleSystem;

        [Header("Initialize battle")] [SerializeField]
        private Image background;

        [SerializeField] private GameObject fighterPrefab;
        [SerializeField] private Transform playerParent0;
        [SerializeField] private Transform playerParent1;
        [SerializeField] private List<Transform> listEnemyParent = new List<Transform>();
        [SerializeField] private Nameplate playerNameplate0;
        [SerializeField] private Nameplate playerNameplate1;
        [SerializeField] private List<NameplateBoss> listEnemyNameplate = new List<NameplateBoss>();
        [SerializeField] private ActionController actionController;
        [SerializeField] private InputsUI inputsUI;
        [SerializeField] private FlashEffect flashEffect;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject looseScreen;

        //To have ref of players animator
        private Animator _animPlayer0;
        private Animator _animPlayer1;
        private List<Animator> _listAnimatorEnemies = new List<Animator>();

        [Header("Gamefeel")] [SerializeField] private Ease nameplateEase;


        public void Initialize(BattleSystem battleSystem, FighterData fighterData0, FighterData fighterData1,
            List<FighterData> enemyData, Sprite arenaSprite)
        {
            if (!battleSystem.OnlyBattlePhaseScene)
                GameManager.Instance.OnChangePhase();

            _battleSystem = battleSystem;

            fighterData0.Fighter.ResetFighter();
            fighterData1.Fighter.ResetFighter();

            for (int x = 0; x < enemyData.Count; x++)
            {
                enemyData[x].Fighter.ResetFighter();
            }

            InitializePlayer(fighterData0, fighterData1);
            InitializeEnemy(enemyData);
            InitializeBattleField(arenaSprite);
            InitializeInputsUI(battleSystem);

            actionController.InitializeActionUI(fighterData0.Fighter.Abilities, fighterData1.Fighter.Abilities,
                _battleSystem);
        }

        public void EnemyNameplateAnim()
        {
            List<RectTransform> listEnemyNameplateRect = new List<RectTransform>();

            switch (_battleSystem.ListEnemiesData.Count)
            {

                case 1:
                    listEnemyNameplateRect.Add(listEnemyNameplate[0].gameObject.GetComponent<RectTransform>());
                    StartCoroutine(NamePlateBossAnim(listEnemyNameplateRect));
                    listEnemyNameplate[1].gameObject.SetActive(false);
                    listEnemyNameplate[2].gameObject.SetActive(false);
                    break;
                case 2:
                    listEnemyNameplateRect.Add(listEnemyNameplate[0].gameObject.GetComponent<RectTransform>());
                    listEnemyNameplateRect.Add(listEnemyNameplate[1].gameObject.GetComponent<RectTransform>());
                    StartCoroutine(NamePlateBossAnim(listEnemyNameplateRect));
                    listEnemyNameplate[2].gameObject.SetActive(false);
                    break;
                case 3:
                    listEnemyNameplateRect.Add(listEnemyNameplate[0].gameObject.GetComponent<RectTransform>());
                    listEnemyNameplateRect.Add(listEnemyNameplate[1].gameObject.GetComponent<RectTransform>());
                    listEnemyNameplateRect.Add(listEnemyNameplate[2].gameObject.GetComponent<RectTransform>());
                    StartCoroutine(NamePlateBossAnim(listEnemyNameplateRect));
                    break;
            }
        }

        private void InitializeInputsUI(BattleSystem battleSystem)
        {
            inputsUI.InitializeInputsUI(battleSystem);
        }

        private void InitializePlayer(FighterData fighterData0, FighterData fighterData1)
        {
            playerNameplate0.Initialize(fighterData0);
            playerNameplate1.Initialize(fighterData1);

            var player0 = _battleSystem.ListPlayersData[0];
            var player1 = _battleSystem.ListPlayersData[1];
            InitPlayer(player0);
            InitPlayer(player1);

            actionController.InitializeAnimator(_animPlayer0, _animPlayer1);
        }

        private void InitPlayer(FighterData fighterData)
        {
            if (fighterData.ID == 0)
            {
                var fighterGo = Instantiate(fighterPrefab, playerParent0, false);
                fighterGo.name = "Player 0";
                fighterData.SetFighterGameObject(fighterGo);
                var image = fighterData.FighterGo.GetComponent<Image>();
                image.sprite = fighterData.Fighter.Sprite;
                _animPlayer0 = image.GetComponent<Animator>();
                _animPlayer0.runtimeAnimatorController = fighterData.Fighter.AnimatorController;
            }
            else
            {
                var fighterGo = Instantiate(fighterPrefab, playerParent1, false);
                fighterGo.name = "Player 1";
                fighterData.SetFighterGameObject(fighterGo);
                var image = fighterData.FighterGo.GetComponent<Image>();
                image.sprite = fighterData.Fighter.Sprite;
                _animPlayer1 = image.GetComponent<Animator>();
                _animPlayer1.runtimeAnimatorController = fighterData.Fighter.AnimatorController;
            }

        }

        private void InitializeEnemy(List<FighterData> enemyData)
        {
            for (int x = 0; x < enemyData.Count; x++)
            {
                listEnemyNameplate[x].Initialize(enemyData[x].Fighter);
                var enemy = enemyData[x];
                var fighterGo = Instantiate(fighterPrefab, listEnemyParent[x], false);
                fighterGo.name = "Enemy " + x.ToString();
                enemy.SetFighterGameObject(fighterGo);
                enemy.FighterGo.transform.localScale = Vector3.one * 0.8f;
                var image = enemy.FighterGo.GetComponent<Image>();
                image.sprite = enemy.Fighter.Sprite;
                _listAnimatorEnemies.Add(image.GetComponent<Animator>());
                _listAnimatorEnemies[x].runtimeAnimatorController = enemy.Fighter.AnimatorController;
            }

            _battleSystem.playerTargetTransform = _battleSystem.ListEnemiesData[0].FighterGo.GetComponent<RectTransform>();
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

        public void ClearAnimatorListEnemies()
        {
            _listAnimatorEnemies.Clear();
        }

        public void ShowPauseMenu()
        {
            pauseScreen.SetActive(true);
        }

        public void SetActiveAbility(FighterData fighterData, bool result)
        {
            actionController.SetActiveAbility_UI(fighterData, result);
        }

        public void SetActiveInputOnPlayer(FighterData fighterData, bool result)
        {
            inputsUI.SetActiveInputOnPlayer(fighterData, result);
        }

        public void SetActiveInputOnEnemy(FighterData fighterData, bool result)
        {
            inputsUI.SetActiveInputOnEnemy(fighterData, result);
        }

        public Abilities ShiftAction(FighterData fighterData)
        {
            return actionController.UpdateCurrentAbility(fighterData);
        }

        public void SetAnimTrigger(FighterData fighterData, string triggerName)
        {
            //if()
                
                
            if (fighterData == _battleSystem.ListPlayersData[0])
                _animPlayer0.SetTrigger(triggerName);
            else if (fighterData == _battleSystem.ListPlayersData[1])
                _animPlayer1.SetTrigger(triggerName);
            else
                _listAnimatorEnemies[fighterData.ID].SetTrigger(triggerName);
        }

        public void LaunchAbility(FighterData fighterData)
        {
            actionController.LaunchAbility(fighterData);
        }

        public Abilities LaunchEnemyAbility(FighterData fighterData)
        {
             return actionController.LaunchEnemyAbility(fighterData);
        }

        public void ResetAnimator()
        {
            actionController.ResetAnimator();
        }

        public void LaunchFlashEffect(FighterData fighterData, Color color)
        {
            flashEffect.StartFlashEffect(fighterData.FighterGo, color);
        }

        public void ShowWinMenu()
        {
            StartCoroutine(WaitTilExploAgain(winScreen));
            //Maybe put anim rendered with another cam
        }

        public void ShowLooseMenu()
        {
            StartCoroutine(WaitTilExploAgain(looseScreen));
            //Maybe put anim rendered with another cam
        }

        public void HidePauseMenu()
        {
            pauseScreen.SetActive(false);
        }

        private IEnumerator WaitTilExploAgain(GameObject screen)
        {
            screen.SetActive(true);
            yield return new WaitForSeconds(2f);
            screen.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            if (_battleSystem.OnlyBattlePhaseScene) yield break;
            
            GameManager.Instance.OnChangePhase();
            _battleSystem.ResetBattleSystem();

        }

        private IEnumerator NamePlateBossAnim(List<RectTransform> listEnemyNameplate)
        {
            foreach (RectTransform rect in listEnemyNameplate)
            {
                rect.DOAnchorPos(new Vector2(0, -60), 2f).SetEase(nameplateEase);
            }

            yield return new WaitForSeconds(3f);

            foreach (RectTransform rect in listEnemyNameplate)
            {
                rect.DOAnchorPos(new Vector2(400, -60), 2f);
            }
        }

        public void NamePlatePlayersAnim()
        {
            playerNameplate0.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(90, 0), 2f).SetEase(nameplateEase);
            playerNameplate1.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 2f).SetEase(nameplateEase);
        }

        public void FadeEscape()
        {
            //Add fade here
            _battleSystem.ResetBattleSystem(); //maybe use waittilExplo 
            throw new NotImplementedException();
        }
    }
}
