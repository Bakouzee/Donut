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
            if (BattleSystem.FighterTurn == BattleSystem.Player0)
            {
                BattleSystem.Interface.SetAnimTrigger(BattleSystem.Player0, "ChooseAbility");
                BattleSystem.Interface.SetActiveAbility(BattleSystem.Player0, false);
                BattleSystem.Interface.SetActivePlayerInput(BattleSystem.Player0, false);
                BattleSystem.Interface.LaunchAbility(BattleSystem.FighterTurn);
            }
            
            yield break;
        }

        public override IEnumerator UseInput_B()
        {
            if (BattleSystem.FighterTurn == BattleSystem.Player1)
            {
                BattleSystem.Interface.SetAnimTrigger(BattleSystem.Player1, "ChooseAbility");
                BattleSystem.Interface.SetActiveAbility(BattleSystem.Player1, false);
                BattleSystem.Interface.SetActivePlayerInput(BattleSystem.Player1, false);
                BattleSystem.Interface.LaunchAbility(BattleSystem.FighterTurn);
            }

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
            BattleSystem.Enemy.Damage(_currentAbility.damage);

            UpdateFighterTurn();
            BattleSystem.Interface.UpdateUI();
            
            if (BattleSystem.Enemy.IsDead)
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
        }

        public void UpdateFighterTurn()
        {
            if(BattleSystem.FighterTurn == BattleSystem.Player0 && !BattleSystem.Player1.IsDead)
                BattleSystem.FighterTurn = BattleSystem.Player1;
            else if(BattleSystem.FighterTurn == BattleSystem.Player1 && !BattleSystem.Player0.IsDead)
                BattleSystem.FighterTurn = BattleSystem.Player0;
        }
    }
}

