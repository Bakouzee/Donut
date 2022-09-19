using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class PlayerTurn : State
    {
        
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            if (BattleSystem.FighterTurn == BattleSystem.Player0)
            {
                BattleSystem.Interface.ShowAction();
                BattleSystem.Interface.ShowInputPlayer0();
            }
            else
            {
                BattleSystem.Interface.ShowAction();
                BattleSystem.Interface.ShowInputPlayer1();
            }
            
            
            yield return new WaitForSeconds(1f);
            BattleSystem.SetState(new ActionTurn(BattleSystem));
        }
    }
}

