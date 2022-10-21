using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class EnemyTurn : State
    {
        private FighterData _targetData;
        private Abilities _currentAbility;
        
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            AttackTarget();
            yield break;
        }

        public override IEnumerator AnimationEnded()
        {
            _targetData.Fighter.Damage(_currentAbility.damage);
            BattleSystem.Interface.UpdateUI();
            if (_targetData.Fighter.IsDead)
            {
                if (CheckIfBothPlayersAreDead())
                {
                    Debug.Log("All PLayers dead");
                    BattleSystem.SetState(new Lost(BattleSystem));
                }

                else
                {
                    Debug.Log("PlayerTurn");
                    BattleSystem.SetState(new PlayerTurn(BattleSystem));
                }
            }
            else
            {
                Debug.Log(_targetData.Fighter.name  + " is alive with" + _targetData.Fighter.CurrentHealth);
                yield return new WaitForSeconds(1);
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
        }
        
        public override IEnumerator HitEffect()
        {
            BattleSystem.Interface.LaunchFlashEffect(_targetData, _currentAbility.hitColor);
            yield break;
        }
        
        private void AttackTarget()
        {
            if (CheckIfBothPlayersAreDead())
            {
                int rand = UnityEngine.Random.Range(0, 2);

                if (rand == 0)
                {
                    _targetData = BattleSystem.ListPlayersData[0];
                    BattleSystem.enemyTargetTransform = BattleSystem.ListPlayersData[0].FighterGo.transform;
                }
                else
                {
                    _targetData = BattleSystem.ListPlayersData[1];
                    BattleSystem.enemyTargetTransform = BattleSystem.ListPlayersData[1].FighterGo.transform;
                }
            }
            else
            {
                _targetData = BattleSystem.CurrentFighterData;

                BattleSystem.enemyTargetTransform = BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[0] ? BattleSystem.ListPlayersData[0].FighterGo.transform : BattleSystem.ListPlayersData[1].FighterGo.transform;
            }
                

            _currentAbility = BattleSystem.Interface.LaunchEnemyAbility(BattleSystem.ListEnemiesData[0]); //Update to random enemy if multiple enemy
            BattleSystem.Interface.LaunchAbility(BattleSystem.ListEnemiesData[0]); //Random attack of enemy
        }

        private bool CheckIfBothPlayersAreDead()
        {
            return !BattleSystem.ListPlayersData[0].Fighter.IsDead && !BattleSystem.ListPlayersData[1].Fighter.IsDead;
        }
        
        

    }
}
