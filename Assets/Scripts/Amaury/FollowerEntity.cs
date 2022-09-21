using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEntity : Character,IFollowable {
    
    
    public Character target { get; set; }
    public float range { get; set; }
    public Direction direction { get; set; }

    public void Start() {
        target = FindObjectOfType<Player>();

        if (target is Player) 
            ((Player)target).followers.Add(this);
    }
    
    
    protected override void Move() {}

    public void OnFollowBegin() {}

    public void Follow() {
        SwitchAnimState(target.currentState);
    }

    public void OnFollowEnd() {}    
}
