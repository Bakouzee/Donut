using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class Begin : State
    {
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //BattleSystem.Interface.SetDialogText($"A wild {BattleSystem.Enemy.Name} appeared!");

            yield return new WaitForSeconds(2f);

            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}

