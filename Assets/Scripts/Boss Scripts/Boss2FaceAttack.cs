using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2FaceAttack : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigidBody;
    [SerializeField] float attackRange = 3f;
    Animator animator;
    AnimatorStateInfo aInfo;

    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, rigidBody.position) <= attackRange)
        {
            animator.SetBool("Attack", true);
            aInfo = animator.GetCurrentAnimatorStateInfo(0);
           if(!aInfo.IsName("boss2attack2") && !aInfo.IsName("boss2attack2 0"))
            {
                if (animator != null)
                {
                    Vector2 playerPos = player.transform.position;
                    Vector2 lookDir = playerPos - rigidBody.position;
                    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                    rigidBody.rotation = angle + 60;
                }
                else
                {
                    Debug.Log("Animator in Boss2FaceAttack is null");
                }
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }

    }
}
