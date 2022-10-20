using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class Abilities : ScriptableObject
{
    public int attackId;
    public string attackName;
    public Sprite iconSprite;
    public string attackDesc;
    public int damage;
    public AnimatorController _animatorController;
    public Color hitColor;
}
