using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //Initialize choose action UI
            //Choose attack
            yield break;
        }

        public override IEnumerator AnimationEnded()
        {
            //Update data interface
            //Update fighter turn if one is dead
            
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
            //Or loose
            yield break;
        }

    }
}
