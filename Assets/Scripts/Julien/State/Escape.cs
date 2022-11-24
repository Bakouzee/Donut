using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class Escape : State
    {
        public Escape(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //BattleSystem.Interface.SetDialogText("You were defeated.");
            BattleSystem.BattleUI.FadeEscape();
            yield break;
        }
    }
}