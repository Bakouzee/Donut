using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Donut.BattleSystem
{
    public class BattleSystem : StateMachine
    {
        [SerializeField] private BattleUI ui;
        [SerializeField] private Sprite arenaSprite;
        [SerializeField] private FighterDatabase fighterDatabase;
        [SerializeField, Range(1, 3)] private int numberOfEnemy;
        [SerializeField] private Player player;

        [SerializeField] private bool onlyBattlePhaseScene;
        public bool OnlyBattlePhaseScene => onlyBattlePhaseScene;
        public Player Player => player;


        [HideInInspector] public static FighterData CurrentFighterData;
        [HideInInspector] public static FighterData CurrentEnemyData;
        [HideInInspector] public static bool CanUseInput = false;
        [HideInInspector] public RectTransform playerTargetTransform;
        [HideInInspector] public RectTransform enemyTargetTransform;
        public BattleUI BattleUI => ui;
        
        public readonly List<FighterData> ListPlayersData = new List<FighterData>();
        public readonly List<FighterData> ListEnemiesData = new List<FighterData>();

        private CheatManager _cheatManager;
        
        public void Start()
        {
            if(onlyBattlePhaseScene)
                SetState(new Init(this)); //Start set in the player collision
            else
            {
                if(ui != null)
                    ui.gameObject.SetActive(false);
            }
 
        }

        public void StartBattlePhaseCinematic(GameObject firstEnemy)
        {
            player.playerInput.ActivateInput();
            SetState((new Init(this)));
            Destroy(firstEnemy);
        }
        public void InitializeBattle()
        {
            ListPlayersData.Add(new FighterData(fighterDatabase.PlayersList[0], 0));
            ListPlayersData.Add(new FighterData(fighterDatabase.PlayersList[1], 1));
            AddEnemiesToList();
            BattleUI.Initialize(this, ListPlayersData[0], ListPlayersData[1], ListEnemiesData, arenaSprite);
            CurrentFighterData = ListPlayersData[0];
            _cheatManager = GetComponent<CheatManager>();
            _cheatManager.Initialize(this);
        }

        private void AddEnemiesToList()
        {
            for (int x = 0; x < numberOfEnemy; x++)
            {
                var enemy = fighterDatabase.EnemiesList[0].Clone();
                ListEnemiesData.Add(new FighterData(enemy as Fighter, (byte)x));
            }
        }
        
        public void ResetBattleSystem()
        {
            Destroy(ListPlayersData[0].FighterGo.gameObject);
            Destroy(ListPlayersData[1].FighterGo.gameObject);

            foreach(FighterData enemy in ListEnemiesData)
            {
                Destroy(enemy.FighterGo.gameObject);
            }
            
            ListEnemiesData.Clear();
            BattleUI.ClearAnimatorListEnemies();
            _cheatManager.ResetInitialization();
        }

        #region Inputs
        public void OnUseInput_A()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_A());
        }

        public void OnUseInput_B()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_B());
        }

        public void OnUseInput_rightArrow()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_rightArrow());
        }
        
        public void OnUseInput_upArrow()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_upArrow());
        }
        
        public void OnUseInput_downArrow()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_downArrow());
        }

        public void HitEffect()
        {
            StartCoroutine(State.HitEffect());
        }

        public void Animation_End()
        {
            StartCoroutine(State.AnimationEnded());
        }
        #endregion Inputs

        #region DebugCheats

        [ContextMenu("DebugCheatPlayer1")]
        public void DebugCheatPlayer1()
        {
            Debug.Log("P0 Invincible : " + ListPlayersData[0].Fighter.IsInvincible);
            Debug.Log("P0 CanOneShot : " + ListPlayersData[0].Fighter.CanOneShot);
            Debug.Log("P0 FullHealth : " + ListPlayersData[0].Fighter.IsFullHealth);
        }
        
        [ContextMenu("DebugCheatPlayer2")]
        public void DebugCheatPlayer2()
        {
            Debug.Log("P1 Invincible : " + ListPlayersData[1].Fighter.IsInvincible);
            Debug.Log("P1 CanOneShot : " + ListPlayersData[1].Fighter.CanOneShot);
            Debug.Log("P1 FullHealth : " + ListPlayersData[1].Fighter.IsFullHealth);
        }
        
        [ContextMenu("DebugCheatEnemies")]
        public void DebugCheatEnemies()
        {

            Debug.Log("All Enemies Invincible : " + ListEnemiesData.All(x => x.Fighter.IsInvincible));
            Debug.Log("All Enemies CanOneShot : " + ListEnemiesData.All(x => x.Fighter.CanOneShot));
            Debug.Log("All Enemies FullHealth : " + ListEnemiesData.All(x => x.Fighter.IsFullHealth));
        }
        

        #endregion
        
        [ContextMenu("DebugEnnemies")]
        public void DebugEnnemies()
        {
            foreach (FighterData enemyData in ListEnemiesData)
            {
                Debug.Log(enemyData.FighterGo.name + " " + enemyData.Fighter.IsDead + " " + "Hp " + enemyData.Fighter.CurrentHealth);
            }
        }

        [ContextMenu("DamageEnemy0")]
        public void DamageEnemy0()
        {
            ListEnemiesData[0].Fighter.Damage(10);
        }
    }
}