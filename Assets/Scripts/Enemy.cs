using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int health = 100;
    [SerializeField] float attackCounter;
    [SerializeField] float maxTimeBetweeAttacks = 3f;
    [SerializeField] float minTimeBetweenAttacks = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile)
        {
            health -= (projectile.GetDamage());

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else 
        {
            Debug.Log("Testies");
        }
    }
}
