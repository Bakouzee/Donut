using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEntity : Character,IFollowable {
    
    public Character target { get; set; }
    public float range { get; set; }
    public Direction direction { get; set; }
    public bool isFollowing { get; set; }
    public bool lastFollow { get; set; }

    public void Start() {
        target = FindObjectOfType<Player>();

        if (target is Player) 
            ((Player)target).followers.Add(this);

        isFollowing = true;

        range = 2;
    }

    public override void Update()
    {
        base.Update();
        
        Debug.Log("update entity");
        
        if(isFollowing && !lastFollow)
            OnFollowBegin();
        
        if(isFollowing)
            Follow();

        if (!isFollowing && lastFollow)
            OnFollowEnd();

        lastFollow = isFollowing;
    }
    
    protected override void Move() {}

    public void OnFollowBegin()
    {
        Debug.Log("follow begin");
    }

    public void Follow() {
        SwitchAnimState(target.currentState);

        transform.position = target.transform.position - transform.right * range;
        spriteRenderer.flipX = target.spriteRenderer.flipX;
    }

    public void OnFollowEnd()
    {
        Debug.Log("follow end");
    }    
}
