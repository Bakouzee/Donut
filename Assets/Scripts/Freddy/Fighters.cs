using Com.Donut.BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighters : MonoBehaviour
{
    [SerializeField] private FighterDatabase database;
    public FighterDatabase Database => database;
    [NonReorderable] public List<Fighter> EnemiesToAdd;
}
