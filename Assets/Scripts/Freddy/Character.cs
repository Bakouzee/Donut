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
    public SpriteRenderer spriteRenderer;
    
    public string currentState;

    public string name;
    
    protected static string[] IDLES = new string[] {"I_Front", "I_Back","I_Side","I_Left" };
    protected static string[] WALKS = new string[] { "W_Front","W_Back","W_Side" }; 
    protected static string[] WALKS_CARAPACE = new string[] { "WC_Front","WC_Back","WC_Side" };

    
    public virtual void Awake() {
        animController = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentState = "I_Front";
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
        if (currentState == newState || newState == null) return;
    
        
        Debug.Log("name " + (name + "_" + newState));

        animController.Play(name + "_" +  newState);
        currentState = newState.Replace(name,"");
    }
}
