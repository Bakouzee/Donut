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
            BattleSystem.Interface.SetAnimTrigger(BattleSystem.Player0, "ChooseAbility");
            BattleSystem.Interface.SetActiveAbility(BattleSystem.Player0, false);
            BattleSystem.Interface.SetActivePlayerInput(BattleSystem.Player0, false);
            BattleSystem.Interface.LaunchAbility(BattleSystem.FighterTurn);
            
            yield break;
        }

        public override IEnumerator UseInput_B()
        {
            BattleSystem.Interface.SetAnimTrigger(BattleSystem.Player1, "ChooseAbility");
            BattleSystem.Interface.SetActiveAbility(BattleSystem.Player1, false);
            BattleSystem.Interface.SetActivePlayerInput(BattleSystem.Player1, false);
            BattleSystem.Interface.LaunchAbility(BattleSystem.FighterTurn);
            yield break;
        }

        public override IEnumerator UseInput_Arrow()
        {
            BattleSystem.Interface.ShiftAction(BattleSystem.FighterTurn);
            yield break;
        }
        
        public void EndOfAnim()
        {
            //Damage
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
            //Or Win
        }
    }
}

