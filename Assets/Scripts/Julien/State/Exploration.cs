using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class Exploration : State
    {
        public Exploration(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //.Interface.HideBattleScene();
            Debug.Log("Exploration State");
            RestoreControls();
            yield break;
        }

        private void RestoreControls()
        {
            //BattleSystem.P.playerInput.SwitchCurrentActionMap("Player");
        }

        public void SetBattlePhase()
        {
            BattleSystem.SetState(new Init(BattleSystem));
        }
    }
}
