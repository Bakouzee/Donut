using System.Collections;

namespace Com.Donut.BattleSystem
{
    public class PlayerTurn : State //Choose next fighter
    {
        
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.Interface.SetActiveAbility(BattleSystem.FighterTurn, true);
            BattleSystem.Interface.SetActivePlayerInput(BattleSystem.FighterTurn, true);

            BattleSystem.SetState(new ActionTurn(BattleSystem));
            
            yield break;
        }
    }
}

