using System.Collections;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class PlayerAction: State
    {
        public PlayerAction(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.CurrentFighterData.SetFighterCurrentAbility(BattleSystem.CurrentFighterData.Fighter.Abilities[0]);
            yield break;
        }
        public override IEnumerator UseInput_A()
        {
            if (CheckPlayer(0))
            {
                DisableInputOnPlayer();
                ChooseAttack();
                BattleSystem.SetState(new PlayerAttack(BattleSystem));
                BattleSystem.CanUseInput = false;
            }

            yield break;
        }


        public override IEnumerator UseInput_B()
        {
            if (CheckPlayer(1))
            {
                DisableInputOnPlayer();
                ChooseAttack();
                BattleSystem.SetState(new PlayerAttack(BattleSystem));
                BattleSystem.CanUseInput = false;
            }
                

            yield break;
        }

        public override IEnumerator UseInput_rightArrow()
        {
            BattleSystem.CurrentFighterData.SetFighterCurrentAbility(BattleSystem.Interface.ShiftAction(BattleSystem.CurrentFighterData));
            yield break;
        }
        

        
        private void DisableInputOnPlayer()
        {
            BattleSystem.Interface.SetActiveInputOnPlayer(BattleSystem.CurrentFighterData, false);
        }

        private bool CheckPlayer(byte id)
        {
            if (BattleSystem.ListPlayersData[0].ID == id)
                return true;
            return false;
        }
        
        private void ChooseAttack()
        {
            BattleSystem.Interface.SetAnimTrigger(BattleSystem.CurrentFighterData, "ChooseAbility");
            BattleSystem.Interface.SetActiveAbility(BattleSystem.CurrentFighterData, false);
        }
    }
}
