using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    [CreateAssetMenu]
    public class AllFighters : ScriptableObject
    {
        //public List<Fighter> fighters = new List<Fighter>();
        [SerializeField] private string _name;
        [SerializeField] private string _level;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private RuntimeAnimatorController _animatorController;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _totalHealth;
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;
        [SerializeField] private int _power;
        [SerializeField] private int _healing;
        private bool _isDead;
        [SerializeField] private List<Abilities> abilities = new List<Abilities>();
        public string Name { get { return _name;} set { _name = value;} }

        public string Level { get { return _level; } set { _level = value; } }
        public Sprite Sprite { get { return _sprite; } set { _sprite = value; } }
        public RuntimeAnimatorController AnimatorController { get { return _animatorController; } set { _animatorController = value; } }
        public int TotalHealth { get { return _totalHealth; } set { _totalHealth = value; } }
        public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
        public int MinDamage { get { return _minDamage; } set { _minDamage = value; } }
        public int MaxDamage { get { return _maxDamage; } set { _maxDamage = value; } }
        public int Power { get { return _power; } set { _power = value; } }
        public int Healing { get { return _healing; } set { _healing = value; } }
        public bool IsDead { get { return _isDead; } set { _isDead = value; } }


        public List<Abilities> Abilities { get { return abilities; } set { abilities = value; } }

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
    }
}
