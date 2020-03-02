using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int health = 100;
    [SerializeField] float attackCounter;
    [SerializeField] float maxTimeBetweeAttacks = 3f;
    [SerializeField] float minTimeBetweenAttacks = 0.2f;
    [SerializeField] GameObject projectile; // if enemy is projectile type. Probably need scripts for each type of enemy.
    [SerializeField] float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
