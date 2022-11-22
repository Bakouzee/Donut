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
        [SerializeField] private Fighter player0;
        [SerializeField] private Fighter player1;
        public List<Fighter> listEnemyFighters = new List<Fighter>();
        [SerializeField] private Player player;
        public Player Player => player;


        [HideInInspector] public static FighterData CurrentFighterData;
        [HideInInspector] public static bool CanUseInput = false;
        [HideInInspector] public Transform playerTargetTransform;
        [HideInInspector] public Transform enemyTargetTransform;
        public BattleUI Interface => ui;
        
        public readonly List<FighterData> ListPlayersData = new List<FighterData>();
        public readonly List<FighterData> ListEnemiesData = new List<FighterData>();

        private CheatManager _cheatManager;
        
        //[ContextMenu("StartBattle")]
        public void StartBattle()
        {
           //SetState((new Init(this))); //Start set in the player collision
        }
        
        public void InitializeBattle()
        {
            ListPlayersData.Add(new FighterData(player0, 0));
            ListPlayersData.Add(new FighterData(player1, 1));
            AddEnemiesToList();
            Interface.Initialize(this, ListPlayersData[0], ListPlayersData[1], ListEnemiesData, arenaSprite);
            CurrentFighterData = ListPlayersData[0];
            _cheatManager = GetComponent<CheatManager>();
            _cheatManager.Initialize(this);
        }

        private void AddEnemiesToList()
        {
            for (int x = 0; x < listEnemyFighters.Count; x++)
            {
                ListEnemiesData.Add(new FighterData(listEnemyFighters[x], (byte)x));
            }
            
            if(listEnemyFighters.Count > 3)
                Debug.LogError("More than 3 enemies --- Impossible");
        }

        //[ContextMenu("ResetBattleSystem")]
        public void ResetBattleSystem()
        {
            Destroy(ListPlayersData[0].FighterGo.gameObject);
            Destroy(ListPlayersData[1].FighterGo.gameObject);

            foreach(FighterData enemy in ListEnemiesData)
            {
                Destroy(enemy.FighterGo.gameObject);
            }
            listEnemyFighters.Clear();
            ListEnemiesData.Clear();
            Interface.ClearAnimatorListEnemies();
            
            _cheatManager.IsInBattle = false;
            _cheatManager.IsInitialized = false;
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
    }
}