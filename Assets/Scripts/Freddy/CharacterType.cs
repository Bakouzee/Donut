using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterType", menuName = "Type", order = 1)]
public class CharacterType : ScriptableObject
{
    public enum Type
    {
        Enemy,
        Player
    };

    public Type type;
    public int life;
    public int attackDmg;
    public int defense;
    public int speed;
    public int luck;
}
