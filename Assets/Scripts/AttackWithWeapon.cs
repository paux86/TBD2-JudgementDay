using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithWeapon : MonoBehaviour
{
    public Transform firePoint;
    public Weapon equippedWeapon;
    int damageScale;
    public LineRenderer lineRenderer;
    public bool inMeleeRange;

    //object references
    private FindNearestTarget nearestEnemy;
    private float nextFireTime;

    private void Start()
    {
        nearestEnemy = gameObject.GetComponent<FindNearestTarget>();
        nextFireTime = Time.time;
        damageScale = levelsCompleted + 1;

        if(equippedWeapon == null)
        {
            Debug.Log("equipped weapon was null, reverting to default weapon on player");
             equippedWeapon = gameObject.GetComponent<PlayerStats>().weaponInventory[2];

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (nearestEnemy.nearestTarget && nearestEnemy.distanceToNearestEnemy <= equippedWeapon.range && Time.time >= nextFireTime)
        {
            if (equippedWeapon.hitscan)
            {
                int mask = ~(1 << gameObject.layer);
                RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, equippedWeapon.range, mask);

                if (hitInfo)
                {
                    TakeDamageInterface enemy = hitInfo.transform.GetComponent<TakeDamageInterface>();


                    if (enemy != null)
<<<<<<< HEAD
                        enemy.TakeDamage(equippedWeapon.attackDamage * damageScale);
                    
=======
                        enemy.TakeDamage(equippedWeapon.attackDamage);

>>>>>>> fef5da3208b79cfe6bf25c0b55fd99e4763d309c

                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, firePoint.position);
                    lineRenderer.SetPosition(1, hitInfo.point);
                }
            }
            else
            {
                if (lineRenderer != null)
                    lineRenderer.enabled = false;
                if (equippedWeapon.isMeleeWeapon && (gameObject.GetComponent<Animator>() != null))
                {
                    inMeleeRange = true;
                    gameObject.GetComponent<Animator>().SetBool("inMeleeRange", inMeleeRange);
                }
                GameObject projectileObject = Instantiate(equippedWeapon.projectile, firePoint.position, firePoint.rotation);
<<<<<<< HEAD
                Projectile projectile =  projectileObject.GetComponent<Projectile>();
                projectile.SetDamage(equippedWeapon.attackDamage * damageScale);
=======
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.SetDamage(equippedWeapon.attackDamage);
>>>>>>> fef5da3208b79cfe6bf25c0b55fd99e4763d309c
            }

            nextFireTime = Time.time + equippedWeapon.attackCooldownTime;
        }
        else
        {
            if (equippedWeapon.isMeleeWeapon && (gameObject.GetComponent<Animator>() != null))
            {
                inMeleeRange = false;
                gameObject.GetComponent<Animator>().SetBool("inMeleeRange", inMeleeRange);
            }
        }
    }
}
