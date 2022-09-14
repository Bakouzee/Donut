using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //BattleSystem.Interface.SetDialogText($"{BattleSystem.Enemy.Name} attacks!");

            //var isDead = BattleSystem.Player.Damage(BattleSystem.Enemy.Attack);

            yield return new WaitForSeconds(1f);

            /*if (isDead)
            {
                BattleSystem.SetState(new Lost(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }*/
        }

    }
}
