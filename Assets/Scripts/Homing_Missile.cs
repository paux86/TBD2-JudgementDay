using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Homing_Missile : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] int damage = 10;

    private Rigidbody2D rb;
    //private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
