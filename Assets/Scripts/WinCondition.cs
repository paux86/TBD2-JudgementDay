using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    bool isWon;

    void Start(){
        isWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        //placeholder code should not cause compilation errors. commenting this out for now.
        /*
        if (wave.isFinished && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) //wave.isFinished placeholder for implementation with Wave Spawner
        {
            isWon = true;
        }
        */
    }
}
