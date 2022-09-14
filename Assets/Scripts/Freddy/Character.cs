using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Character : MonoBehaviour
{
    protected int life;
    protected int attackDmg;
    protected int defense;
    protected int speed;
    protected int luck;

    protected void InitCharacter(CharacterType characterToInit)
    {
        this.life = characterToInit.life;
        this.attackDmg = characterToInit.attackDmg;
        this.defense = characterToInit.defense;
        this.speed = characterToInit.speed;
        this.luck = characterToInit.luck;
    }

}
