using System;
using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class Init : State //Initialize Battle
    {
        public Init(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            StopPlayerControls();
            BattleSystem.InitializeBattle();
            BattleSystem.SetState(new Intro(BattleSystem));
            yield break;
        }

        private void StopPlayerControls()
        {
            BattleSystem.P.playerInput.SwitchCurrentActionMap("BattlePhase");
        }
    }
}

