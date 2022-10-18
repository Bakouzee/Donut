using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class EnemyTurn : State
    {
        private Fighter _target;
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
            Debug.Log("AnimationEnded");
            _target.Damage(_currentAbility.damage);
            if (_target.IsDead)
            {
                if (BattleSystem.Player0.IsDead && BattleSystem.Player1.IsDead)
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
                Debug.Log("Enemy alive with" + _target.CurrentHealth);
                yield return new WaitForSeconds(1);
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
        }
        
        private void AttackTarget()
        {
            if (!BattleSystem.Player1.IsDead && !BattleSystem.Player0.IsDead)
            {
                int rand = UnityEngine.Random.Range(0, 2);

                if (rand == 0)
                {
                    _target = BattleSystem.Player0;
                    BattleSystem.enemyTargetTransform = BattleSystem.player0Go.transform;
                }
                else
                {
                    _target = BattleSystem.Player1;
                    BattleSystem.enemyTargetTransform = BattleSystem.player1Go.transform;
                }
            }
            else
            {
                _target = BattleSystem.FighterTurn;

                BattleSystem.enemyTargetTransform = BattleSystem.FighterTurn == BattleSystem.Player0 ? BattleSystem.player0Go.transform : BattleSystem.player1Go.transform;
            }
                

            _currentAbility = BattleSystem.Interface.LaunchEnemyAbility(BattleSystem.Enemy);
            BattleSystem.Interface.LaunchAbility(BattleSystem.Enemy); //Random attack of enemy
        }

    }
}
