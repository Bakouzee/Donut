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
        
        public virtual IEnumerator Attack() //Input A Mario
        {
            yield break;
        }

        public virtual IEnumerator Heal() //Input B Luigi
        {
            yield break;
        }
    }
}

