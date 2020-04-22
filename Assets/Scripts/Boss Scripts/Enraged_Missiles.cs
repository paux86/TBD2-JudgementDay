using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enraged_Missiles : MonoBehaviour
{
    [SerializeField] GameObject missile = default;
    [SerializeField] float offset = 3f;
    [SerializeField] int numberOfMissiles = 10;

    Transform pos;
    Vector3 posWithOffset;

    private void Start()
    {
        pos = gameObject.transform;
    }

    public void Fire()
    {
        for (int i = 0; i < numberOfMissiles; i++)
        {
            posWithOffset = pos.position;
            posWithOffset.x += Random.Range(-offset, offset);
            posWithOffset.y += Random.Range(-offset, offset);
            Instantiate(missile, posWithOffset, pos.rotation);
        }
    }
}
