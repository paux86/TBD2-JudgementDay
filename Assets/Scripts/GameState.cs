using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
   
    [SerializeField] int numLevels;
    [SerializeField] bool[] levelsComplete;
    private EnemySpawner enemySpawner;
    private SceneLoader sceneLoader;
    int loopstuff;


    private void Start()
    {
        levelsComplete = new bool[numLevels];
        UpdateObjects();
        loopstuff = 0;
    }

    private void Update()
    {
       

        if((GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) && (enemySpawner.isWavesComplete()))
        {
            enemySpawner.SetWaveComplete(false);
            sceneLoader.ChangeToLevelSelect();
            StartCoroutine(Wait());

        }

   
    }

    private void Awake()
    {
        int gameStateCount = FindObjectsOfType<GameState>().Length;
        if (gameStateCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void UpdateObjects()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>().GetComponent<EnemySpawner>();
        sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(8);
        Debug.Log("test");
        UpdateObjects();

    }
}
