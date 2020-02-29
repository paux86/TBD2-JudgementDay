using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestEnemy : MonoBehaviour
{
    public GameObject nearestEnemy;
    public float distanceToNearestEnemy;

    // Update is called once per frame
    void Update()
    {
        findNearest();
    }

    void findNearest()
    {
        distanceToNearestEnemy = Mathf.Infinity;
        nearestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject currentEnemy in enemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - gameObject.transform.position).sqrMagnitude;
            if(distanceToEnemy < distanceToNearestEnemy)
            {
                distanceToNearestEnemy = distanceToEnemy;
                nearestEnemy = currentEnemy;
            }
        }

        if(nearestEnemy)
            Debug.DrawLine(gameObject.transform.position, nearestEnemy.transform.position);
    }
}
