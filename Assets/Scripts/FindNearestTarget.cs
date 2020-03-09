using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestTarget : MonoBehaviour
{
    public GameObject nearestTarget;
    public float distanceToNearestEnemy;

    // Update is called once per frame
    void Update()
    {
        findNearest();
    }

    void findNearest()
    {
        distanceToNearestEnemy = Mathf.Infinity;
        nearestTarget = null;
        GameObject[] targets;

        if (gameObject.tag == "Enemy")
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
        }
        else
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");
        }

        foreach(GameObject currentTarget in targets)
        {
            float distanceToEnemy = (currentTarget.transform.position - gameObject.transform.position).sqrMagnitude;
            if(distanceToEnemy < distanceToNearestEnemy)
            {
                distanceToNearestEnemy = distanceToEnemy;
                nearestTarget = currentTarget;
            }
        }

        if(nearestTarget)
            Debug.DrawLine(gameObject.transform.position, nearestTarget.transform.position);
    }
}
