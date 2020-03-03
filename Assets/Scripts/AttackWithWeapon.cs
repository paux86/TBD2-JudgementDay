using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithWeapon : MonoBehaviour
{
    public Transform firePoint;
    public Weapon equippedWeapon;

    //object references
    private FindNearestEnemy nearestEnemy;
    private float nextFireTime;

    private void Start()
    {
        nearestEnemy = gameObject.GetComponent<FindNearestEnemy>();
        nextFireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(nearestEnemy.nearestEnemy && nearestEnemy.distanceToNearestEnemy <= equippedWeapon.range && Time.time >= nextFireTime)
        {
            if (equippedWeapon.hitscan)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

                if (hitInfo)
                {
                    //todo   
                }
            }
            else
            {
                
                GameObject projectileObject = Instantiate(equippedWeapon.projectile, firePoint.position, firePoint.rotation);
                Projectile projectile =  projectileObject.GetComponent<Projectile>();
                projectile.SetDamage(equippedWeapon.GetAttackDamage());
            }

            nextFireTime = Time.time + equippedWeapon.attackCooldownTime;
        }
    }
}
