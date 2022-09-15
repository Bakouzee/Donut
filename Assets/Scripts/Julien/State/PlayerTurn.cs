using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.Interface.ShowAction(); //Show action UI and enable input
            yield break;
        }

        public override IEnumerator UseInput_A()
        {
            //Launch attack
            yield break;
        }

        public override IEnumerator UseInput_B()
        {
            //Launch attack
            yield break;
        }

        public override IEnumerator UseInput_Arrow()
        {
            //Update show action
            yield break;
        }
        
        public void EndOfAnim()
        {
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }
}

