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
            BattleSystem.BattleUI.SetActiveAbility(BattleSystem.CurrentFighterData, true);
            BattleSystem.BattleUI.SetActiveInputOnPlayer(BattleSystem.CurrentFighterData, true);
            BattleSystem.CanUseInput = true;

            BattleSystem.SetState(new PlayerAction(BattleSystem));

            yield break;
        }
    }
}

