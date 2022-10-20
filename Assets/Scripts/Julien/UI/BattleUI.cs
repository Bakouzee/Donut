using System.Collections.Generic;
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
        [SerializeField] private List<Transform> listEnemyParent = new List<Transform>();
        [SerializeField] private Nameplate playerNameplate0;
        [SerializeField] private Nameplate playerNameplate1;
        [SerializeField] private List<NameplateBoss> listEnemyNameplate = new List<NameplateBoss>();
        [SerializeField] private ActionController actionController;
        [SerializeField] private FlashEffect flashEffect;
        [SerializeField] private Text dialogText;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject looseScreen;
        
        //To have ref of players animator
        private Animator _animPlayer0;
        private Animator _animPlayer1;
        private List<Animator> _listAnimatorEnemies = new List<Animator>();

        [Header("DisplayInput_UI")]
        [SerializeField] private Image input0;
        [SerializeField] private Image input1;
        [SerializeField] private Animator animInput0;
        [SerializeField] private Animator animInput1;


        public void Initialize(BattleSystem battleSystem, FighterData fighterData0, FighterData fighterData1, List<FighterData> enemyData, Sprite arenaSprite)
        {
            _battleSystem = battleSystem;
            
            fighterData0.fighter.ResetFighter(); //Reset scriptable object to default value
            fighterData1.fighter.ResetFighter();

            for (int x = 0; x < enemyData.Count; x++)
            {
                enemyData[x].fighter.ResetFighter();
            }
            
            InitializePlayer(fighterData0, fighterData1);
            InitializeEnemy(enemyData);
            InitializeBattleField(arenaSprite);
            
            actionController.InitializeActionUI(fighterData0.fighter.Abilities, fighterData1.fighter.Abilities, _battleSystem);
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
                fighterData.FighterGo  = Instantiate(fighterPrefab, playerParent0, false);
                var image = fighterData.FighterGo.GetComponent<Image>();
                image.sprite = fighterData.fighter.Sprite;
                _animPlayer0 = image.GetComponent<Animator>();
                _animPlayer0.runtimeAnimatorController = fighterData.fighter.AnimatorController;
            }
            else
            {   
                fighterData.FighterGo  = Instantiate(fighterPrefab, playerParent1, false);
                var image = fighterData.FighterGo.GetComponent<Image>();
                image.sprite = fighterData.fighter.Sprite;
                _animPlayer1 = image.GetComponent<Animator>();
                _animPlayer1.runtimeAnimatorController = fighterData.fighter.AnimatorController;
            }

        }

        private void InitializeEnemy(List<FighterData> enemyData)
        {
            for (int x = 0; x < enemyData.Count; x++)
            {
                listEnemyNameplate[0].Initialize(enemyData[x].fighter);
                var enemy = enemyData[x];
                enemy.FighterGo =  Instantiate(fighterPrefab, listEnemyParent[x], false);
                enemy.FighterGo.transform.localScale = Vector3.one * 0.8f;
                var image = enemy.FighterGo.GetComponent<Image>();
                image.sprite = enemy.fighter.Sprite;
                _listAnimatorEnemies.Add(image.GetComponent<Animator>());
                _listAnimatorEnemies[x].runtimeAnimatorController = enemy.fighter.AnimatorController;
            }
            
            _battleSystem.playerTargetTransform = _battleSystem.ListEnemiesData[0].FighterGo.transform;
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
        
        public void SetActiveAbility(FighterData fighterData, bool result)
        {
            actionController.SetActiveAbility_UI(fighterData, result);
        }

        public void SetActivePlayerInput(FighterData fighterData, bool result)
        {
            if(fighterData == _battleSystem.ListPlayersData[0])
                input0.gameObject.SetActive(result);
            else
                input1.gameObject.SetActive(result);
        }

        public Abilities ShiftAction(FighterData fighterData)
        {
            return actionController.UpdateCurrentAbility(fighterData);
        }

        public void SetAnimTrigger(FighterData fighterData, string triggerName)
        {
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
            winScreen.SetActive(true);
            //Maybe put anim rendered with another cam
        }

        public void ShowLooseMenu()
        {
            looseScreen.SetActive(true);
            //Maybe put anim rendered with another cam
        }
        
        public void HidePauseMenu()
        {
            pauseScreen.SetActive(false);
        }
    }
}
