using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class ActionTurn : State
    {
        private Abilities _currentAbility;
        public ActionTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            _currentAbility = BattleSystem.FighterTurn.Abilities[0];
            yield break;
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
            _currentAbility = BattleSystem.Interface.ShiftAction(BattleSystem.FighterTurn);
            yield break;
        }
        
        public override IEnumerator AnimationEnded()
        {
            Debug.Log("AnimationEnded");
            BattleSystem.Interface.ResetAnimator();
            //Damage
            if (BattleSystem.Enemy.Damage(_currentAbility.damage))
            {
                Debug.Log("Enemy Dead");
                BattleSystem.SetState(new Won(BattleSystem));
            }
            else
            {
                Debug.Log("Enemy alive with" + BattleSystem.Enemy.CurrentHealth);
                yield return new WaitForSeconds(1);
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }
            
            yield break;
        }
    }
}

