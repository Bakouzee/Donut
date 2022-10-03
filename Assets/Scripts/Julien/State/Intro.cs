using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class Intro : State //Starting battle anim
    {
        public Intro(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //Launch all anim or let time for anim

            yield return new WaitForSeconds(2f);

            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}

