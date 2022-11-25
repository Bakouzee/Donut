using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Com.Donut.BattleSystem
{
    public class PlayerAttack: State
    {
        private FighterData _currentTargetData;
        private FighterData _previousTargetData;

        private Abilities _fighterAbility;

        private List<int> _listEnemyAliveIndex = new List<int>();
        public PlayerAttack(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            _fighterAbility = BattleSystem.CurrentFighterData.CurrentAbility;
            
            _listEnemyAliveIndex = CheckEnemyAlive();

            if (_listEnemyAliveIndex.Count > 1 && _fighterAbility.actionType == Abilities.ActionType.Damage)               //Check si il y a plus d'un enemi en vie
            {
                _currentTargetData = _listEnemyAliveIndex.Contains(1) ? BattleSystem.ListEnemiesData[1] : BattleSystem.ListEnemiesData[0];
                BattleSystem.BattleUI.SetActiveInputOnEnemy(_currentTargetData, true);
                BattleSystem.playerTargetTransform = _currentTargetData.FighterGo.GetComponent<RectTransform>();
                _previousTargetData = _currentTargetData;
            }
            else                                      //Tester si un seul enemy est en vie 
            {
                BattleSystem.CanUseInput = false;
                _currentTargetData = BattleSystem.ListEnemiesData[_listEnemyAliveIndex[0]];
                BattleSystem.playerTargetTransform = _currentTargetData.FighterGo.GetComponent<RectTransform>();
                Attack();
            }

            yield break;
        }
        public override IEnumerator UseInput_A()
        {
            if (!CheckPlayer(0) || _currentTargetData.Fighter.IsDead) yield break;
            Attack();
            BattleSystem.BattleUI.SetActiveInputOnEnemy(_currentTargetData, false);
            BattleSystem.CanUseInput = false;
        }


        public override IEnumerator UseInput_B()
        {
            if (!CheckPlayer(1) || _currentTargetData.Fighter.IsDead) yield break;
            Attack();
            BattleSystem.BattleUI.SetActiveInputOnEnemy(_currentTargetData, false);
            BattleSystem.CanUseInput = false;
        }

        public override IEnumerator UseInput_upArrow()
        {
            if (_listEnemyAliveIndex.Count <= 1 || _fighterAbility.actionType != Abilities.ActionType.Damage) yield break;
            
            if (_currentTargetData == BattleSystem.ListEnemiesData[2] && !BattleSystem.ListEnemiesData[0].Fighter.IsDead)
                TargetEnemy(0);
            else if(_currentTargetData == BattleSystem.ListEnemiesData[2] && BattleSystem.ListEnemiesData[0].Fighter.IsDead && !BattleSystem.ListEnemiesData[1].Fighter.IsDead)
                TargetEnemy(1);
            else if (_currentTargetData == BattleSystem.ListEnemiesData[0] && !BattleSystem.ListEnemiesData[1].Fighter.IsDead)
                TargetEnemy(1);
        }
        
        public override IEnumerator UseInput_downArrow()
        {
            if (_listEnemyAliveIndex.Count <= 1 || _fighterAbility.actionType != Abilities.ActionType.Damage) yield break;
            
            if (_currentTargetData == BattleSystem.ListEnemiesData[1] && !BattleSystem.ListEnemiesData[0].Fighter.IsDead)
                TargetEnemy(0);
            else if (_currentTargetData == BattleSystem.ListEnemiesData[1] && BattleSystem.ListEnemiesData[0].Fighter.IsDead && !BattleSystem.ListEnemiesData[2].Fighter.IsDead)
                TargetEnemy(2);
            else if (_currentTargetData == BattleSystem.ListEnemiesData[0] && !BattleSystem.ListEnemiesData[2].Fighter.IsDead)
                 TargetEnemy(2);
        }
        
        public override IEnumerator AnimationEnded()
        {
            BattleSystem.BattleUI.ResetAnimator();
            var enemy = _currentTargetData;
            BattleSystem.CurrentEnemyData = enemy;

            switch (_fighterAbility.actionType)  
            {
                case Abilities.ActionType.Damage:
                    enemy.Fighter.Damage(BattleSystem.CurrentFighterData.Fighter.CanOneShot
                        ? int.MaxValue
                        : _fighterAbility.amount);
                    break;
                case Abilities.ActionType.Heal:
                    BattleSystem.CurrentFighterData.Fighter.Heal(_fighterAbility.amount); //Anim Heal
                    break;
                case Abilities.ActionType.Escape:
                    BattleSystem.SetState(new Escape(BattleSystem));
                    break;
            }

            UpdateFighterTurn();

            if (enemy.Fighter.IsDead)
            {
                Debug.Log("Enemy Dead");
                BattleSystem.BattleUI.SetAnimTrigger(enemy, "Dead");
            }
            else
            {
                Debug.Log("Enemy alive with" + enemy.Fighter.CurrentHealth);
                yield return new WaitForSeconds(1);
            }

            if (CheckWin())
                BattleSystem.SetState(new Won(BattleSystem));
            else
                BattleSystem.SetState(new EnemyTurn(BattleSystem));

        }
        
        public override IEnumerator HitEffect()
        {
            BattleSystem.BattleUI.LaunchFlashEffect(_currentTargetData, BattleSystem.CurrentFighterData.CurrentAbility.hitColor); 
            yield break;
        }
        
        private void Attack()
        {
            BattleSystem.BattleUI.LaunchAbility(BattleSystem.CurrentFighterData);
        }

        private void UpdateFighterTurn() //If both player are alive, change fighter turn, else let same fighter
        {
            if(BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[0] && !BattleSystem.ListPlayersData[1].Fighter.IsDead)
                BattleSystem.CurrentFighterData = BattleSystem.ListPlayersData[1];
            else if(BattleSystem.CurrentFighterData == BattleSystem.ListPlayersData[1] && !BattleSystem.ListPlayersData[0].Fighter.IsDead)
                BattleSystem.CurrentFighterData = BattleSystem.ListPlayersData[0];
        }
        
        private bool CheckPlayer(byte id)
        {
            return BattleSystem.CurrentFighterData.ID == id;
        }
        private bool CheckWin()
        {
            var numberEnemyDead = 0;

            foreach(var enemy in BattleSystem.ListEnemiesData)
            {
                if(enemy.Fighter.IsDead)
                    numberEnemyDead++;
            }
            
            Debug.Log("number dead : " + numberEnemyDead);

            return numberEnemyDead == BattleSystem.ListEnemiesData.Count;
        }

        private List<int> CheckEnemyAlive()
        {
            var listEnemyAliveIndex = new List<int>();
            var index = 0;
            foreach (FighterData enemyData in BattleSystem.ListEnemiesData)
            {
                if (!enemyData.Fighter.IsDead)
                {
                    listEnemyAliveIndex.Add(index);
                }
                index++;
            }

            return listEnemyAliveIndex;
        }

        private void TargetEnemy(byte index)
        {
            BattleSystem.BattleUI.SetActiveInputOnEnemy(_previousTargetData, false);
            _currentTargetData = BattleSystem.ListEnemiesData[index];
            BattleSystem.BattleUI.SetActiveInputOnEnemy(_currentTargetData, true);
            BattleSystem.playerTargetTransform = _currentTargetData.FighterGo.GetComponent<RectTransform>();
            _previousTargetData = _currentTargetData;
        }

    }
}
