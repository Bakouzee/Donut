using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class ActionTurn : State
    {
        public ActionTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator UseInput_A()
        {
            //Launch attack
            BattleSystem.Interface.HideAction();
            BattleSystem.Interface.HideInputPlayers();
            yield break;
        }

        public override IEnumerator UseInput_B()
        {
            //Launch attack
            BattleSystem.Interface.HideAction();
            BattleSystem.Interface.HideInputPlayers();
            yield break;
        }

        public override IEnumerator UseInput_Arrow()
        {
            BattleSystem.Interface.ShiftAction();
            yield break;
        }
        
        public void EndOfAnim()
        {
            
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
            //Or Win
        }
    }
}

