using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    [CreateAssetMenu]
    public class FighterDatabase : ScriptableObject
    {
        [NonReorderable] //Fix bug list in inspector
        public List<Fighter> PlayersList;
        
        [NonReorderable]
        public List<Fighter> EnemiesList;
    }

}
