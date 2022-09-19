using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class BattleSystem : StateMachine
    {
        [SerializeField] private BattleUI ui;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Fighter player0;
        [SerializeField] private Fighter player1;
        [SerializeField] private Fighter enemy;

        [HideInInspector] public static Fighter FighterTurn;
        [HideInInspector] public static bool CanUseInput = false;

        public Fighter Player0 => player0;
        public Fighter Player1 => player1;
        public Fighter Enemy => enemy;
        public BattleUI Interface => ui;
        public Sprite Sprite => sprite;

        private void Start()
        {
            SetState(new Init(this));
        }

        public void InitializeBattle()
        {
            Interface.Initialize(player0, player1, enemy, sprite);
            FighterTurn = player0;
        }
        
        
        public void useInput_A()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_A());
        }

        public void useInput_B()
        {
            if(CanUseInput)
                StartCoroutine(State.UseInput_B());
        }

        public void UseInput_Arrow()
        {
            //if(CanUseInput)
                StartCoroutine(State.UseInput_Arrow());
        }
    }
}