using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Character : MonoBehaviour
{
    protected struct Position
    {
        public float x;
        public float y;
    }

    protected Position position;

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

    protected abstract void Move();

    // If we use some kind of grid or whatever
    protected Vector2 GetPosition()
    {
        position = RefreshPos();
        return new Vector2(position.x, position.y);
    }

    private Position RefreshPos()
    {
        Position newPos = position;
        if(TryGetComponent<Transform>(out Transform pos))
        {
            newPos.x = pos.position.x; 
            newPos.y = pos.position.y; 
        }
        return newPos;
    }
}
