using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class Lost : State
    {
        public Lost(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.BattleUI.fadeEffect.Fader();
            yield return new WaitForSeconds(BattleSystem.BattleUI.fadeEffect.FadeDuration);
            //BattleSystem.Interface.SetDialogText("You were defeated.");
            BattleSystem.BattleUI.ShowLooseMenu();
            yield break;
        }
    }
}