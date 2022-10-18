using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class Won : State
    {
        public Won(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.Interface.ShowWinMenu();
            // For now
            BattleSystem.SetState(new Exploration(BattleSystem));
            yield break;
        }

    }
}
