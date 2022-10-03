using System.Collections;

namespace Com.Donut.BattleSystem
{
    public abstract class State
    {
        protected BattleSystem BattleSystem;

        public State(BattleSystem battleSystem)
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
        
        public virtual IEnumerator UseInput_Arrow() 
        {
            yield break;
        }
    }
}

