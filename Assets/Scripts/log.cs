using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{
    private Rigidbody2D rigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = EnemyState.idle;
        rigidbody = GetComponent <Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        
        if(Vector3.Distance(target.position, transform.position)<= chaseRadius && Vector3.Distance(target.position, transform.position) >= attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState!= EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                Vector2 move = temp - transform.position;
                ChangeAnim(move);
                animator.SetBool("moving", true);
                rigidbody.MovePosition(temp);                
                ChangeState(EnemyState.walk);
            }
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void ChangeAnim(Vector2 direction)
    {
        if (direction != Vector2.zero && currentState== EnemyState.walk)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
        
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }

    }

}
