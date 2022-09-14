using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class BattleSystem : StateMachine
    {
        /*[SerializeField] private BattleUI ui;
        [SerializeField] private Fighter player;
        [SerializeField] private Fighter enemy;
        
        public Fighter Player => player;
        public Fighter Enemy => enemy;
        public BattleUI Interface => ui;*/

        private void Start()
        {
            //Interface.Initialize(player, enemy);

            SetState(new Begin(this));
        }

        public void OnAttackButton()
        {
            StartCoroutine(State.Attack());
        }

        public void OnHealButton()
        {
            StartCoroutine(State.Heal());
        }
    }
}