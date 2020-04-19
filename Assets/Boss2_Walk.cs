using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Walk : StateMachineBehaviour
{
    Rigidbody2D rigidBody;
    [SerializeField] float yAxisBottom = -7f;
    [SerializeField] float yAxisTop = 7f;
    [SerializeField] float speed = 5f;
    Vector2 target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidBody = animator.GetComponent<Rigidbody2D>();
       if(rigidBody.position.y == yAxisTop)
        {
            target = new Vector2(rigidBody.position.x, yAxisBottom);
        }
       else
        {
            target = new Vector2(rigidBody.position.x, yAxisTop);
        }
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        rigidBody.isKinematic = true;
        animator.SetBool("doneWalking", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newPos = Vector2.MoveTowards(rigidBody.position, target, speed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPos);
        if(rigidBody.position.y == yAxisBottom && animator.GetBool("atTop"))
        {
            rigidBody.rotation = 180;
            animator.SetBool("atTop", false);
            animator.SetBool("atBottom", true);
            animator.SetBool("doneWalking", true);
        }
        else if ( rigidBody.position.y == yAxisTop && animator.GetBool("atBottom"))
        {
            rigidBody.rotation = 360;
            animator.SetBool("atTop", true);
            animator.SetBool("atBottom", false);
            animator.SetBool("doneWalking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
