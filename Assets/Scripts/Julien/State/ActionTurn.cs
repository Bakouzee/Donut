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
            _currentAbility = BattleSystem.CurrentFighterData.fighter.Abilities[0];
            yield break;
        }
        public override IEnumerator UseInput_A()
        {
            if (BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[0])
            {
                LaunchAttack();
                DisableInputUI();
            }
            
            yield break;
        }


        public override IEnumerator UseInput_B()
        {
            if (BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[1])
            {
                LaunchAttack();
                DisableInputUI();
            }

            yield break;
        }

        public override IEnumerator UseInput_Arrow()
        {
            _currentAbility = BattleSystem.Interface.ShiftAction(BattleSystem.CurrentFighterData);
            yield break;
        }
        
        public override IEnumerator AnimationEnded()
        {
            Debug.Log("AnimationEnded");
            
            BattleSystem.Interface.ResetAnimator();
            var enemy = BattleSystem.ListEnemiesData[0]; //1st enemy for the moment, after we will implement target enemy state
            Debug.Log(enemy.fighter.CurrentHealth);
            enemy.fighter.Damage(_currentAbility.damage);

            UpdateFighterTurn();

            if (enemy.fighter.IsDead)
            {
                Debug.Log("Enemy Dead");
                BattleSystem.SetState(new Won(BattleSystem));
            }
            else
            {
                Debug.Log("Enemy alive with" + enemy.fighter.CurrentHealth);
                yield return new WaitForSeconds(1);
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }
        }
        
        private void DisableInputUI()
        {
            BattleSystem.Interface.SetActivePlayerInput(BattleSystem.CurrentFighterData, false);
        }

        private void LaunchAttack()
        {
            BattleSystem.Interface.SetAnimTrigger(BattleSystem.CurrentFighterData, "ChooseAbility");
            BattleSystem.Interface.SetActiveAbility(BattleSystem.CurrentFighterData, false);
            BattleSystem.Interface.LaunchAbility(BattleSystem.CurrentFighterData);
        }


        public override IEnumerator HitEffect()
        {
            BattleSystem.Interface.LaunchFlashEffect(BattleSystem.ListEnemiesData[0], _currentAbility.hitColor); //1st enemy for the moment, after we will implement target enemy state
            yield break;
        }

        public void UpdateFighterTurn() //If both player are alive, change fighter turn, else let same fighter
        {
            if(BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[0] && !BattleSystem.ListPlayersData[1].fighter.IsDead)
                BattleSystem.CurrentFighterData = BattleSystem.ListPlayersData[1];
            else if(BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[1] && !BattleSystem.ListPlayersData[0].fighter.IsDead)
                BattleSystem.CurrentFighterData = BattleSystem.ListPlayersData[0];
        }
    }
}

