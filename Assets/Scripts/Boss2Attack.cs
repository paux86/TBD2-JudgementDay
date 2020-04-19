using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Attack : MonoBehaviour
{
    [SerializeField] int attackDamage = 20;
    [SerializeField] Vector3 attackOffset = default;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask = default;
#pragma warning disable 0649
    [SerializeField] GameObject spitProjectile;
#pragma warning restore 0649

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D collInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (collInfo != null)
        {
           
            collInfo.GetComponent<PlayerStats>().TakeDamage(attackDamage);
            
        }
    }

    public void SpitAttack()
    {

    }
}
