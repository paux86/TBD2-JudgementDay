﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemiesDefeated : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("There are no more enemies!");
        }
    }
}
