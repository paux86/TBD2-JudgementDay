using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 20;
    [SerializeField] int enragedAttackDamage = 40;
    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask;
    
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D collInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if(collInfo != null)
        {
            collInfo.GetComponent<PlayerStats>().TakeDamage(attackDamage);
        }
    }
}
