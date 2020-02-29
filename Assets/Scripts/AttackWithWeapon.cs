using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithWeapon : MonoBehaviour
{
    public Transform firePoint;
    public Weapon equippedWeapon;

    //object references
    public FindNearestEnemy nearestEnemy;

    private void Start()
    {
        nearestEnemy = gameObject.GetComponent<FindNearestEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(nearestEnemy.nearestEnemy && nearestEnemy.distanceToNearestEnemy <= equippedWeapon.range)
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
                Instantiate(equippedWeapon.projectile, firePoint.position, firePoint.rotation);
            }
        }
    }
}
