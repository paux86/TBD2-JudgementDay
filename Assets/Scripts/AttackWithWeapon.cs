using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithWeapon : MonoBehaviour
{
    public Transform firePoint;
    public Weapon equippedWeapon;
    public LineRenderer lineRenderer;

    //object references
    private FindNearestTarget nearestEnemy;
    private float nextFireTime;

    private void Start()
    {
        nearestEnemy = gameObject.GetComponent<FindNearestTarget>();
        nextFireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(nearestEnemy.nearestTarget && nearestEnemy.distanceToNearestEnemy <= equippedWeapon.range && Time.time >= nextFireTime)
        {
            if (equippedWeapon.hitscan)
            {
                int mask = ~(1 << gameObject.layer);
                RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, equippedWeapon.range, mask);

                if (hitInfo)
                {
                    TakeDamageInterface enemy = hitInfo.transform.GetComponent<TakeDamageInterface>();
                   

                    if (enemy != null)
                        enemy.TakeDamage(equippedWeapon.attackDamage);
                    

                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, firePoint.position);
                    lineRenderer.SetPosition(1, hitInfo.point);
                }
            }
            else
            {
                if(lineRenderer != null)
                    lineRenderer.enabled = false;
                GameObject projectileObject = Instantiate(equippedWeapon.projectile, firePoint.position, firePoint.rotation);
                Projectile projectile =  projectileObject.GetComponent<Projectile>();
                projectile.SetDamage(equippedWeapon.attackDamage);
            }

            nextFireTime = Time.time + equippedWeapon.attackCooldownTime;
        }
    }
}
