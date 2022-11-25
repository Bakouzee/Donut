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
            BattleSystem.BattleUI.fadeEffect.Fader();
            yield return new WaitForSeconds(BattleSystem.BattleUI.fadeEffect.FadeDuration);
            BattleSystem.BattleUI.ShowWinMenu();
            yield break;
        }

    }
}
