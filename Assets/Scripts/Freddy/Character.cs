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
    protected Animator animController;
    protected string currentState;

    public virtual void Start() {
        animController = GetComponent<Animator>();
    }

    public virtual void Update() {
        Move();
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

    protected void SwitchAnimState(string newState) {
        if (currentState == newState) return;
        
        animController.Play(newState);

        currentState = newState;
    }
}