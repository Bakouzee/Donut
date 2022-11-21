using System.Collections.Generic;
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
        
        private void Start()
        {
           //SetState((new Init(this)));
        }
        
        public void InitializeBattle()
        {
            ListPlayersData.Add(new FighterData(player0, 0));
            ListPlayersData.Add(new FighterData(player1, 1));
            AddEnemiesToList();
            Interface.Initialize(this, ListPlayersData[0], ListPlayersData[1], ListEnemiesData, arenaSprite);
            CurrentFighterData = ListPlayersData[0];
            GetComponent<CheatManager>().Initialize(this);
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
    }
}