using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public abstract class State
    {
        protected readonly BattleSystem BattleSystem;

        protected State(BattleSystem battleSystem)
        {
            BattleSystem = battleSystem;
        }
        public virtual IEnumerator Start()
        {
            yield break;
        }
        
        public virtual IEnumerator UseInput_A() 
        {
            yield break;
        }

        public virtual IEnumerator UseInput_B() 
        {
            yield break;
        }
        
        public virtual IEnumerator UseInput_rightArrow() 
        {
            yield break;
        }
        
        public virtual IEnumerator UseInput_upArrow() 
        {
            yield break;
        }
        
        public virtual IEnumerator UseInput_downArrow() 
        {
            yield break;
        }

        public virtual IEnumerator HitEffect()
        {
            yield break;
        }

        public virtual IEnumerator AnimationEnded()
        {
            yield break;
        }
    }
}

