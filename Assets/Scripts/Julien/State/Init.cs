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
            if(!BattleSystem.OnlyBattlePhaseScene)
                StopPlayerControls();

            BattleSystem.BattleUI.fadeEffect.Fader();
            yield return new WaitForSeconds(BattleSystem.BattleUI.fadeEffect.FadeDuration);

            BattleSystem.InitializeBattle();
            BattleSystem.SetState(new Intro(BattleSystem));
            yield break;
        }
        
        private void StopPlayerControls()
        {
            BattleSystem.Player.playerInput.SwitchCurrentActionMap("BattlePhase");
        }
    }
}

