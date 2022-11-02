using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolNew : enemyAi
{

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
 
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void checkDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {

            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);
            rb2D.MovePosition(temp);

           
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if(Vector3.Distance(transform.position, path[currentPoint].position)  > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                rb2D.MovePosition(temp);
            }
            else
            {
                changeGoal();
            }
         
        }
    }

    private void changeGoal()
    {
        if(currentPoint == path.Length -1)
        {
           
            
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            anim.SetBool("wakeUp", true);
            currentPoint++;
            currentGoal = path[currentPoint];    
        }
    }
}
