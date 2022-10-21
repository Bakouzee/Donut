using Com.Donut.BattleSystem;
using UnityEngine;

public class FighterData
{
    public FighterData(Fighter fg, byte id)
    {
        fighter = fg;
        ID = id;
    }
    public Fighter fighter { get; }
    public GameObject FighterGo { get; private set; }
    public readonly byte ID;

    public void SetFighterGameObject(GameObject go)
    {
        FighterGo = go;
    }
}
