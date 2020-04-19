using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Attack : MonoBehaviour
{
    [SerializeField] int attackDamage = 20;
    [SerializeField] Vector3 attackOffset = default;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask = default;
    [SerializeField] float numOfSpitProjectiles = 30;
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
        
      if (spitProjectile != null)
        {
            float degree = 360f / numOfSpitProjectiles;
            for (float i = -180; i < 180f; i+= degree)
            {
                Quaternion rotation = Quaternion.AngleAxis(i , transform.forward);
                Vector3 shotPosition = gameObject.transform.GetChild(0).position;
                GameObject projectileObject = Instantiate(spitProjectile,shotPosition , rotation * transform.rotation);
            }
            
        }
      else
        {
            Debug.LogError("spitProjectile is null in Boss2Attack");
        }
    }

    
    
}
