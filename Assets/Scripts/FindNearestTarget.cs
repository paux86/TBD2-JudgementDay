using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestTarget : MonoBehaviour
{
    public GameObject nearestTarget;
    public float distanceToNearestEnemy;
    [SerializeField] string selfTag = default;
    [SerializeField] string targetTag = default;

    // Update is called once per frame//
    void Update()
    {
        findNearest();
    }

    void findNearest()
    {
        distanceToNearestEnemy = Mathf.Infinity;
        nearestTarget = null;
        GameObject[] targets;

        if (gameObject.tag == targetTag)
        {
            targets = GameObject.FindGameObjectsWithTag(selfTag);
        }
        else
        {
            targets = GameObject.FindGameObjectsWithTag(targetTag);
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
