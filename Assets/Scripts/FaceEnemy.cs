using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private FindNearestEnemy nearestEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        nearestEnemy = gameObject.GetComponent<FindNearestEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(nearestEnemy.nearestEnemy)
        {
            Vector2 enemyPos = nearestEnemy.nearestEnemy.transform.position;
            Vector2 lookDir = enemyPos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
