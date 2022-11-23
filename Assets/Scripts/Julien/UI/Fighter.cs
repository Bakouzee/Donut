using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    [System.Serializable]
    public class Fighter
    {
        [SerializeField] private string _name;
        [SerializeField] private string _level;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private RuntimeAnimatorController _animatorController;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _totalHealth;
        //[SerializeField] private int _currentDamage;
        //[SerializeField] private int _baseDamage;
        [SerializeField] private int _power;
        [SerializeField] private int _healing;
        [SerializeField] private List<Abilities> abilities = new List<Abilities>();
        private bool _isInvincible;
        private bool _canOneShot;
        private bool _isDead;
        private bool _isFullHealth;


        public List<Abilities> Abilities => abilities;
        public string Name => _name;
        public string Level => _level;
        public Sprite Sprite => _sprite;
        public RuntimeAnimatorController AnimatorController => _animatorController;
        public int TotalHealth => _totalHealth;
        public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
        public int Power => _power;
        public int Healing => _healing;
        public bool IsDead => _isDead;
        public bool IsInvincible => _isInvincible;
        public bool CanOneShot => _canOneShot;
        public bool IsFullHealth => _isFullHealth;

        public void Damage(int amount)
        {
            if (_isInvincible || _isFullHealth) { return; }

            _currentHealth = Math.Max(0, _currentHealth - amount);
            
            if (_currentHealth == 0)
            {
                _isDead = true;
            }
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;

            if (_currentHealth > _totalHealth)
                _currentHealth = _totalHealth;
        }

        public void ResetFighter()
        {
            _currentHealth = _totalHealth;
            _isDead = false;
            _isInvincible = false;
            _canOneShot = false;
            _isFullHealth = false;
        }
        

        #region Cheats
        public void SetInvincible(bool result)
        {
            _isInvincible = result;
        }

        public void SetOneShotEnemies(bool result)
        {
            _canOneShot = result;
        }

        public void ResetHealth(bool result)
        {
            if(result)
                _currentHealth = _totalHealth;
    
            _isFullHealth = result;
        }

        #endregion Cheats
    }
}