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
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;
        [SerializeField] private int _power;
        [SerializeField] private int _healing;
        private bool _isDead;
        
        [SerializeField] private List<Abilities> abilities = new List<Abilities>();

        public string Name => _name;
        public string Level => _level;
        public Sprite Sprite => _sprite;
        public AnimatorController AnimatorController => _animatorController;
        public int TotalHealth => _totalHealth;
        public int CurrentHealth => _currentHealth;
        public int MinDamage => _minDamage;
        public int MaxDamage => _maxDamage; 
        public int Power => _power;
        public int Healing => _healing;
        public bool IsDead => _isDead; 
        public List<Abilities> Abilities => abilities;

        public void Damage(int amount)
        {
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

        private void OnValidate()
        {
            _currentHealth = Math.Min(_currentHealth, _totalHealth);
        }

        public void ResetFighter()
        {
            _currentHealth = _totalHealth;
            _isDead = false;
        }
    }
}