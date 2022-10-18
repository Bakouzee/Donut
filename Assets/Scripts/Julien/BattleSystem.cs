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
        [SerializeField] private Player p;
        [SerializeField] private LayerMask layersToKeep;

        public GameObject player0Go;
        public GameObject player1Go;
        public GameObject enemy1Go;

        [HideInInspector] public static Fighter FighterTurn;
        public Transform playerTargetTransform;
        public Transform enemyTargetTransform;
        [HideInInspector] public static bool CanUseInput = false;

        public Fighter Player0 => player0;
        public Fighter Player1 => player1;
        public Fighter Enemy => enemy;
        public Player P => p;
        public BattleUI Interface => ui;
        public Sprite Sprite => sprite;
        public LayerMask LayersToKeep => layersToKeep;


        private void Start()
        {
            //SetState(new Exploration(this));
        }

        public void InitializeBattle()
        {
            Interface.Initialize(this, player0, player1, enemy, sprite);
            FighterTurn = player0;
        }
        
        
        public void OnUseInput_A(InputAction.CallbackContext ctx)
        {
            if(CanUseInput && ctx.performed)
                StartCoroutine(State.UseInput_A());
        }

        public void OnUseInput_B(InputAction.CallbackContext ctx)
        {
            if(CanUseInput && ctx.performed)
                StartCoroutine(State.UseInput_B());
        }

        public void OnUseInput_Arrow(InputAction.CallbackContext ctx)
        {
            if(CanUseInput && ctx.performed)
                StartCoroutine(State.UseInput_Arrow());
        }

        public void Animation_End()
        {
            StartCoroutine(State.AnimationEnded());
        }
    }
}