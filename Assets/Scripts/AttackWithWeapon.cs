using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithWeapon : MonoBehaviour
{
    public Transform firePoint;
    public Weapon equippedWeapon;
    public LineRenderer lineRenderer;
    public Animator animator;
    public bool inMeleeRange = false;

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
                    //needs to be optimized
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    Boss boss = hitInfo.transform.GetComponent<Boss>();
                    PlayerStats player = hitInfo.transform.GetComponent<PlayerStats>();

                    if (enemy != null)
                        enemy.TakeDamage(equippedWeapon.attackDamage);
                    else if (boss != null)
                        boss.TakeDamage(equippedWeapon.attackDamage);
                    else if (player != null)
                    {
                        Debug.Log("Hit player");
                        player.TakeDamage(equippedWeapon.attackDamage);
                    }

                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, firePoint.position);
                    lineRenderer.SetPosition(1, hitInfo.point);
                }
            }
            else
            {
                if(lineRenderer != null)
                    lineRenderer.enabled = false;
                if(equippedWeapon.isMelee)
                {
                    inMeleeRange = true;
                    animator.SetBool("inMeleeRange", inMeleeRange);
                }
                GameObject projectileObject = Instantiate(equippedWeapon.projectile, firePoint.position, firePoint.rotation);
                Projectile projectile =  projectileObject.GetComponent<Projectile>();
                projectile.SetDamage(equippedWeapon.attackDamage);
            }

            nextFireTime = Time.time + equippedWeapon.attackCooldownTime;
        }
        else
        {
            inMeleeRange = false;
            animator.SetBool("inMeleeRange", inMeleeRange);
        }
    }
}
