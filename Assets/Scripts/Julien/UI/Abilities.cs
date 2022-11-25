using UnityEngine;

[CreateAssetMenu]
public class Abilities : ScriptableObject
{
    public enum ActionType
    {
        Damage,
        Heal,
        Escape,
    }
    
    public int attackId;
    public string attackName;
    public Sprite iconSprite;
    public string attackDesc;
    public ActionType actionType;
    public int amount;
    public RuntimeAnimatorController _animatorController;
    public Color hitColor;
}
