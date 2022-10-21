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
        [SerializeField] private List<Fighter> listEnemyFighters = new List<Fighter>();

        [HideInInspector] public static FighterData CurrentFighterData;
        [HideInInspector] public static bool CanUseInput = false;
        public Transform playerTargetTransform;
        public Transform enemyTargetTransform;
        public BattleUI Interface => ui;

        public readonly List<FighterData> ListPlayersData = new List<FighterData>();
        public readonly List<FighterData> ListEnemiesData = new List<FighterData>();
        
        private void Start()
        {
            SetState((new Init(this)));
        }
        
        public void InitializeBattle()
        {
            ListPlayersData.Add(new FighterData(player0, 0));
            ListPlayersData.Add(new FighterData(player1, 1));
            AddEnemiesToList();
            Interface.Initialize(this, ListPlayersData[0], ListPlayersData[1], ListEnemiesData, arenaSprite);
            CurrentFighterData = ListPlayersData[0];
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

        public void OnUseInput_Arrow()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_Arrow());
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