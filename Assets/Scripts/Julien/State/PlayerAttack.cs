using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Donut.BattleSystem
{
    public class PlayerAttack: State
    {
        private FighterData _currentTargetData;
        private FighterData _previousTargetData;

        private int _numberEnemyAlive;
        public PlayerAttack(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            CheckEnemyAlive();

            if (_numberEnemyAlive > 1)               //Check si il y a plus d'un enemi en vie
            {
                _currentTargetData = BattleSystem.ListEnemiesData[1];
                BattleSystem.Interface.SetActiveInputOnEnemy(_currentTargetData, true);
                BattleSystem.playerTargetTransform = _currentTargetData.FighterGo.transform;
                _previousTargetData = _currentTargetData;
            }

            else //Tester si un seul enemy est en vie
            {
                BattleSystem.CanUseInput = false;
                _currentTargetData = BattleSystem.ListEnemiesData[0];
                BattleSystem.playerTargetTransform = _currentTargetData.FighterGo.transform;
                Attack();
            }

            yield break;
        }
        public override IEnumerator UseInput_A()
        {
            if (CheckPlayer(0) && !_currentTargetData.Fighter.IsDead)
            {
                Attack();
                BattleSystem.Interface.SetActiveInputOnEnemy(_currentTargetData, false);
                BattleSystem.CanUseInput = false;
            }
            else if (CheckPlayer(0) && _currentTargetData.Fighter.IsDead)
            {
                Debug.Log("Cant attack dead enemies");
            }
                
            
            yield break;
        }


        public override IEnumerator UseInput_B()
        {
            if (CheckPlayer(1) && !_currentTargetData.Fighter.IsDead)
            {
                Attack();
                BattleSystem.Interface.SetActiveInputOnEnemy(_currentTargetData, false);
                BattleSystem.CanUseInput = false;
            }
            else if (CheckPlayer(1) && _currentTargetData.Fighter.IsDead)
            {
                Debug.Log("Cant attack dead enemies");
            }

            yield break;
        }

        public override IEnumerator UseInput_upArrow()
        {
            if(_numberEnemyAlive == 3)
            {
                if (_currentTargetData == BattleSystem.ListEnemiesData[2])
                    TargetEnemy(0);
                else if (_currentTargetData == BattleSystem.ListEnemiesData[0])
                    TargetEnemy(1);
            }
            else
            {
                if (_currentTargetData == BattleSystem.ListEnemiesData[0])
                    TargetEnemy(1);
            }



            yield break;
        }
        
        public override IEnumerator UseInput_downArrow()
        {
            if (_numberEnemyAlive == 3)
            {
                if (_currentTargetData == BattleSystem.ListEnemiesData[1])
                    TargetEnemy(0);

                else if (_currentTargetData == BattleSystem.ListEnemiesData[0])
                    TargetEnemy(2);
            }
            else
            {
                if (_currentTargetData == BattleSystem.ListEnemiesData[1])
                    TargetEnemy(0);
            }

            yield break;
        }
        
        public override IEnumerator AnimationEnded()
        {
            Debug.Log("AnimationEnded");
            
            BattleSystem.Interface.ResetAnimator();
            var enemy = _currentTargetData;
            Debug.Log(enemy.Fighter.CurrentHealth);
            var playerAbility = BattleSystem.CurrentFighterData.CurrentAbility;

            if(BattleSystem.CurrentFighterData.Fighter.CanOneShot)
                enemy.Fighter.Damage(int.MaxValue);
            else
                enemy.Fighter.Damage(playerAbility.damage);

            UpdateFighterTurn();

            if (enemy.Fighter.IsDead)
            {
                Debug.Log("Enemy Dead");
                BattleSystem.Interface.SetAnimTrigger(enemy, "Dead");
                BattleSystem.SetState(new Won(BattleSystem));
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
            BattleSystem.Interface.LaunchFlashEffect(_currentTargetData, BattleSystem.CurrentFighterData.CurrentAbility.hitColor); //1st enemy for the moment, after we will implement target enemy state
            yield break;
        }
        
        private void Attack()
        {
            BattleSystem.Interface.LaunchAbility(BattleSystem.CurrentFighterData);
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
            if (BattleSystem.CurrentFighterData.ID == id)
                return true;
            return false;
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

            if (numberEnemyDead == BattleSystem.ListEnemiesData.Count)
                return true;
            return false;
        }

        private void CheckEnemyAlive()
        {
            foreach (var enemy in BattleSystem.ListEnemiesData)
            {
                if (!enemy.Fighter.IsDead)
                {
                    _numberEnemyAlive++;
                }
            }
        }

        private void TargetEnemy(byte index)
        {
            BattleSystem.Interface.SetActiveInputOnEnemy(_previousTargetData, false);
            _currentTargetData = BattleSystem.ListEnemiesData[index];
            BattleSystem.Interface.SetActiveInputOnEnemy(_currentTargetData, true);
            BattleSystem.playerTargetTransform = _currentTargetData.FighterGo.transform;
            _previousTargetData = _currentTargetData;
            
        }
        
    }
}
