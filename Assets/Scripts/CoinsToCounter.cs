using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsToCounter : MonoBehaviour
{
    [SerializeField] GameObject collectionPoint;
    private Vector3 worldSpaceCollectionPoint;

    private void Start()
    {
        if(collectionPoint == null)
            collectionPoint = GameObject.Find("MoneyCounter");
        worldSpaceCollectionPoint =  Camera.main.ScreenToWorldPoint(collectionPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, worldSpaceCollectionPoint, 1.5f * Time.deltaTime);
    }
}
