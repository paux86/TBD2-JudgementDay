using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float travelDistance = -1;
    private Rigidbody2D rb;
    private Vector2 startingPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        startingPos = transform.position;
    }

    private void FixedUpdate()
    {
        if(travelDistance >= 0)
        {
            Vector2 currentPos = transform.position;
            if ((currentPos - startingPos).sqrMagnitude >= travelDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
