using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Intro : StateMachineBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] float travelDistance = 0f;
    private Rigidbody2D rb;
    private Vector2 target;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        target = new Vector2(rb.position.x, rb.position.y + travelDistance);
        animator.tag = "Untargetable";
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.tag = "Enemy";
    }
}
