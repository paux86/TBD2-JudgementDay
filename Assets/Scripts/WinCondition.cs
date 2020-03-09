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
        if(wave.isFinished && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) //wave.isFinished placeholder for implementation with Wave Spawner
        {
            isWon = true;
        }
    }
}
