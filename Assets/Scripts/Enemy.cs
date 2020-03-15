using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int maxHealth = 100;
    [SerializeField] float attackCounter;
    [SerializeField] float maxTimeBetweeAttacks = 3f;
    [SerializeField] float minTimeBetweenAttacks = 0.2f;
    [SerializeField] int money = 1;

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().IncrementMoneyCount(money);
        Destroy(gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile)
        {
            Debug.Log("Testies");
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
    }*/
}
