using System.Collections;

namespace Com.Donut.BattleSystem
{
    public class Init : State //Initialize Battle
    {
        public Init(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.InitializeBattle();
            BattleSystem.SetState(new Intro(BattleSystem));
            yield break;
        }
    }
}

