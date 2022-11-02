using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : Enemy
{
    public Rigidbody2D rb2D;
    public Transform target;
    public Transform homePos;
    public float chaseRadius;
    public float moveSpeed;
    public Animator anim;
       


    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag(("Player")).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkDistance();
    }
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }
    public void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }
     public virtual void checkDistance()
    {
        if(Vector3.Distance(target.position,transform.position) <= chaseRadius)
        {
           if(currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                rb2D.MovePosition(temp);
                changeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    private void changeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
