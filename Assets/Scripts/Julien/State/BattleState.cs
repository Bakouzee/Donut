using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class BattleState : State
    {
        public BattleState(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //BattleSystem.Interface.SetDialogText("Choose an action.");
            yield break;
        }

        public override IEnumerator Attack()
        {
            //var isDead = BattleSystem.Enemy.Damage(BattleSystem.Player.Attack);

            yield return new WaitForSeconds(1f);

            /*if (isDead)
            {
                BattleSystem.SetState(new Won(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }*/
        }

        public override IEnumerator Heal()
        {
            //BattleSystem.Interface.SetDialogText($"{BattleSystem.Player.Name} feels renewed strength!");

            //BattleSystem.Player.Heal(5);

            yield return new WaitForSeconds(1f);

            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }
}

