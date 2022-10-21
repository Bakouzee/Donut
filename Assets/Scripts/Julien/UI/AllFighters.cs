using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    [CreateAssetMenu]
    public class AllFighters : ScriptableObject
    {
        public List<Fighter> fighters = new List<Fighter>();
    }
}
