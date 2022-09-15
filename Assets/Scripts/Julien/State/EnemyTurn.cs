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
            //Initialize choose action UI
            //Choose attack

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

        public void EndOfAnim()
        {
            //Update data interface
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }

    }
}
