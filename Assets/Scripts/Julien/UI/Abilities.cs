using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class Abilities : ScriptableObject
{
    public enum ActionType
    {
        Damage,
        Heal,
    }
    public int attackId;
    public string attackName;
    public Sprite iconSprite;
    public string attackDesc;
    public ActionType actionType;
    public int amount;
    public AnimatorController _animatorController;
    public Color hitColor;
}
