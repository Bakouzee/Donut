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
            //BattleSystem.Interface.SetDialogText("You were defeated.");
            BattleSystem.Interface.ShowLooseMenu();
            yield break;
        }
    }
}