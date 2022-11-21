using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    [CreateAssetMenu]
    public class FighterDatabase : ScriptableObject
    {
        public List<Fighter> fighterList;
    }

}
