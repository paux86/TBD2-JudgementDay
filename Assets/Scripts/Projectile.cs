using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float travelDistance = -1;
    private Rigidbody2D rb;
    private int damage = 100;
    private Vector2 startingPos;
    [SerializeField] int longRangeMin = 50;
    
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
        

        TakeDamageInterface enemy = collision.gameObject.GetComponent<TakeDamageInterface>();
       

        if (enemy != null)
        {
            Vector2 currentPos = transform.position;
            float currentTravelDistance = (currentPos - startingPos).sqrMagnitude;
            if (travelDistance < longRangeMin)
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                float longRangeDamage = CalculateLongRangeDamage(currentTravelDistance);
                enemy.TakeDamage((int)longRangeDamage);
            }
        }
           
        
            

        Destroy(gameObject);
    }

    private float CalculateLongRangeDamage(float currentTravelDistance)
    {
        float longRangeDamage;
        if (currentTravelDistance < 50f)
        {
            float halfDamage = damage / 2;
            longRangeDamage = halfDamage;

        }
        else
        {
            float scaledDamage = (damage * (currentTravelDistance / longRangeMin));
            longRangeDamage = scaledDamage;

        }

        return longRangeDamage;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage()
    {
        return this.damage;
    }
}
