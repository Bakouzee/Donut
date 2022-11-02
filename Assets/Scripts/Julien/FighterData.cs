using System.Collections.Generic;
using Com.Donut.BattleSystem;
using UnityEngine;

public class FighterData
{
    public FighterData(Fighter fg, byte id)
    {
        Fighter = fg;
        ID = id;
    }
    
    public Fighter Fighter { get; }
    public GameObject FighterGo { get; private set; }
    public Abilities CurrentAbility { get; private set; }
    
    public readonly byte ID;

    public void SetFighterGameObject(GameObject go)
    {
        FighterGo = go;
    }

    public void SetFighterCurrentAbility(Abilities ability)
    {
        CurrentAbility = ability;
    }
}
