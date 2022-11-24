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
            }
                

            yield break;
        }

        public override IEnumerator UseInput_rightArrow()
        {
            BattleSystem.CurrentFighterData.SetFighterCurrentAbility(BattleSystem.BattleUI.ShiftAction(BattleSystem.CurrentFighterData));
            yield break;
        }
        
        private void DisableInputOnPlayer()
        {
            BattleSystem.BattleUI.SetActiveInputOnPlayer(BattleSystem.CurrentFighterData, false);
        }

        private bool CheckPlayer(byte id)
        {
            return BattleSystem.CurrentFighterData.ID == id;
        }
        
        private void ChooseAttack()
        {
            BattleSystem.BattleUI.SetAnimTrigger(BattleSystem.CurrentFighterData, "ChooseAbility");
            BattleSystem.BattleUI.SetActiveAbility(BattleSystem.CurrentFighterData, false);
        }
    }
}

