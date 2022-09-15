using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [SerializeField] private List<Attack> attacks = new List<Attack>();

        public string Name => _name;
        public string Level => _level;
        public Sprite Sprite => _sprite;
        public AnimatorController AnimatorController => _animatorController;
        public int CurrentHealth => _currentHealth;
        public int TotalHealth => _totalHealth;
        public int MinDamage => _minDamage;
        public int MaxDamage => _maxDamage; 
        public int Power => _power;
        public int Healing => _healing;

        public List<Attack> Attacks => attacks;

        public bool Damage(int amount)
        {
            _currentHealth = Math.Max(0, _currentHealth - amount);
            return _currentHealth == 0;
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;
        }

        private void OnValidate()
        {
            _currentHealth = Math.Min(_currentHealth, _totalHealth);
        }
    }
}