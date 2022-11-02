using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class PlayerAttack: State
    {
        private FighterData _currentTargetData;
        private FighterData _previousTargetData;
        public PlayerAttack(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            if (BattleSystem.ListEnemiesData.Count == 1)
            {
                //BattleSystem.CanUseInput = false;
                _currentTargetData = BattleSystem.ListEnemiesData[0];
                Attack();
            }
            else
            {
                _currentTargetData = BattleSystem.ListEnemiesData[1]; //Enemy 1 is on top so we want to target him first
                BattleSystem.Interface.SetActiveInputOnEnemy(_currentTargetData, true);
                _previousTargetData = BattleSystem.ListEnemiesData[1];
            }

            yield break;
        }
        public override IEnumerator UseInput_A()
        {
            if (CheckPlayer(0))
                Attack();
            
            yield break;
        }


        public override IEnumerator UseInput_B()
        {
            if (CheckPlayer(1))
                Attack();
            
            yield break;
        }

        public override IEnumerator UseInput_upArrow()
        {
            if (_currentTargetData == BattleSystem.ListEnemiesData[2])
                TargetEnemy(0);
            else if (_currentTargetData == BattleSystem.ListEnemiesData[0])
                TargetEnemy(1);

            yield break;
        }
        
        public override IEnumerator UseInput_downArrow()
        {
            if (_currentTargetData == BattleSystem.ListEnemiesData[1])
                TargetEnemy(0);
           
            else if (_currentTargetData == BattleSystem.ListEnemiesData[0])
                TargetEnemy(2);

            yield break;
        }
        
        public override IEnumerator AnimationEnded()
        {
            Debug.Log("AnimationEnded");
            
            BattleSystem.Interface.ResetAnimator();
            var enemy = _currentTargetData;
            Debug.Log(enemy.Fighter.CurrentHealth);
            var playerAbility = BattleSystem.CurrentFighterData.CurrentAbility;
            enemy.Fighter.Damage(playerAbility.damage);

            UpdateFighterTurn();

            if (enemy.Fighter.IsDead)
            {
                Debug.Log("Enemy Dead");
                BattleSystem.SetState(new Won(BattleSystem));
            }
            else
            {
                Debug.Log("Enemy alive with" + enemy.Fighter.CurrentHealth);
                yield return new WaitForSeconds(1);
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }
        }
        
        public override IEnumerator HitEffect()
        {
            BattleSystem.Interface.LaunchFlashEffect(BattleSystem.ListEnemiesData[0], BattleSystem.CurrentFighterData.CurrentAbility.hitColor); //1st enemy for the moment, after we will implement target enemy state
            yield break;
        }
        
        private void Attack()
        {
            BattleSystem.Interface.LaunchAbility(BattleSystem.CurrentFighterData);
        }

        private void UpdateFighterTurn() //If both player are alive, change fighter turn, else let same fighter
        {
            if(BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[0] && !BattleSystem.ListPlayersData[1].Fighter.IsDead)
                BattleSystem.CurrentFighterData = BattleSystem.ListPlayersData[1];
            else if(BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[1] && !BattleSystem.ListPlayersData[0].Fighter.IsDead)
                BattleSystem.CurrentFighterData = BattleSystem.ListPlayersData[0];
        }
        
        private bool CheckPlayer(byte id)
        {
            if (BattleSystem.ListPlayersData[0].ID == id)
                return true;
            return false;
        }

        private void TargetEnemy(byte index)
        {
            BattleSystem.Interface.SetActiveInputOnEnemy(_previousTargetData, false);
            _currentTargetData = BattleSystem.ListEnemiesData[index];
            _previousTargetData = _currentTargetData;
            BattleSystem.Interface.SetActiveInputOnEnemy(_previousTargetData, true);
        }
        
    }
}
