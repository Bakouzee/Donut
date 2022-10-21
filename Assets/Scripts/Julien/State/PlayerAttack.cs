using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class PlayerAttack: State
    {
        public PlayerAttack(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            yield break;
        }
        public override IEnumerator UseInput_A()
        {
            yield break;
        }


        public override IEnumerator UseInput_B()
        {
            yield break;
        }

        public override IEnumerator UseInput_Arrow()
        {
            yield break;
        }
        
        public override IEnumerator AnimationEnded()
        {
            yield break;
        }
        
    }
}
