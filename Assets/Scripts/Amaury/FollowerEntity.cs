using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FollowerEntity : Character,IFollowable {
    
    public Character target { get; set; }
    public float range { get; set; }
    public Direction direction { get; set; }
    public bool isFollowing { get; set; }
    public bool lastFollow { get; set; }

    private NavMeshAgent agent;
    public void Start() {
        target = FindObjectOfType<Player>();

        if (target is Player) 
            ((Player)target).followers.Add(this);

        isFollowing = true;

        range = 2;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public override void Update() {
        base.Update();
        
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
        if (target is Player) {
            Player player = (Player)target;

            Vector3 pMove = player.movement;

            Vector3 direction = pMove.x > 0 && pMove.y == 0 ? transform.right :
                pMove.x < 0 && pMove.y == 0 ? transform.right * -1 : pMove.x == 0 && pMove.y > 0 ? transform.up : transform.up * -1;
            
            agent.SetDestination(target.transform.position - direction * range);
                
            if(player.movement != Vector2.zero)
                SwitchAnimState(target.currentState);
            else {
                if(agent.remainingDistance < 0.5f)
                    SwitchAnimState(target.currentState);
            }
        }
        
        spriteRenderer.flipX = target.spriteRenderer.flipX;
    }

    public void OnFollowEnd() {
        Debug.Log("follow end");
    }    
}
