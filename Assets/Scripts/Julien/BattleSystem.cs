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

        [HideInInspector] public static bool canUseInput = false;

        public Fighter Player0 => player0;
        public Fighter Player1 => player1;
        public Fighter Enemy => enemy;
        public BattleUI Interface => ui;

        public Sprite Sprite => sprite;

        private void Start()
        {
            Interface.Initialize(player0, player1, enemy, sprite);

            SetState(new Begin(this));
        }


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                useInput_A();
            
            if(Input.GetKeyDown(KeyCode.B))
                useInput_B();
            
            if(Input.GetKeyDown(KeyCode.RightArrow))
                UseInput_Arrow();
        }
        public void useInput_A()
        {
            if(canUseInput)
                StartCoroutine(State.UseInput_A());
        }

        public void useInput_B()
        {
            if(canUseInput)
                StartCoroutine(State.UseInput_B());
        }

        public void UseInput_Arrow()
        {
            if(canUseInput)
                StartCoroutine(State.UseInput_Arrow());
        }
    }
}