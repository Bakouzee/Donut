using Com.Donut.BattleSystem;
using UnityEngine;

public class FighterData
{
    public FighterData(Fighter fg, byte id)
    {
        fighter = fg;
        ID = id;
    }
    public Fighter fighter { get;}
    public GameObject FighterGo;
    public readonly byte ID;
}
