using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public bool isWon;
    EnemySpawner enemySpawner;
    [SerializeField] GameObject winText;

    void Start(){
        isWon = false;
        enemySpawner = FindObjectOfType<EnemySpawner>().GetComponent<EnemySpawner>();
        // if(enemySpawner == NULL)
        // {
        //     Debug.Log("Enemy Spawner not initialized!");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySpawner.isFinishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 &! isWon) //wave.isFinished placeholder for implementation with Wave Spawner
        {
            isWon = true;
            GameObject winTextBox = (GameObject)Instantiate(winText);
            Debug.Log("You Won!");
        }
    }
}
