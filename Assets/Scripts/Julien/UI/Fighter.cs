using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    [CreateAssetMenu]
    public class Fighter : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _level;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private AnimatorController _animatorController;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _totalHealth;
        //[SerializeField] private int _currentDamage;
        //[SerializeField] private int _baseDamage;
        [SerializeField] private int _power;
        [SerializeField] private int _healing;
        private bool _isInvincible;
        private bool _canOneShot;
        private bool _isDead;

        [SerializeField] private List<Abilities> abilities = new List<Abilities>();

        public string Name => _name;
        public string Level => _level;
        public Sprite Sprite => _sprite;
        public AnimatorController AnimatorController => _animatorController;
        public int CurrentHealth => _currentHealth;
        //public int CurrentDamage => _currentDamage;
        public int Power => _power;
        public int Healing => _healing;
        public bool IsDead => _isDead;
        public bool IsInvincible => _isInvincible;
        public bool CanOneShot => _canOneShot;
        public List<Abilities> Abilities => abilities;

        public void Damage(int amount)
        {
            if (_isInvincible) { return; }

            _currentHealth = Math.Max(0, _currentHealth - amount);
            if (_currentHealth == 0)
            {
                _isDead = true;
            }
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;
        }

        public void ResetFighter()
        {
            //_currentDamage = _baseDamage;
            _currentHealth = _totalHealth;
            _isDead = false;
            _isInvincible = false;
            _canOneShot = false;
        }

        /*private void OnValidate()
        {
            _currentHealth = Math.Min(_currentHealth, _totalHealth);
        }*/

        #region Cheats
        public void SetInvincible(bool result)
        {
            _isInvincible = result;
        }

        public void SetOneShotEnemies(bool result)
        {
            _canOneShot = result;
        }

        public void ResetHealth()
        {
            _currentHealth = _totalHealth;
        }

        #endregion Cheats
    }
}