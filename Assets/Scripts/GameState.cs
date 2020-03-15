using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
   
    [SerializeField] int numLevels;
    [SerializeField] bool[] levelsComplete;
    private EnemySpawner enemySpawner;
    private SceneLoader sceneLoader;


    private void Start()
    {
        levelsComplete = new bool[SceneManager.sceneCountInBuildSettings];
        if(SceneManager.GetActiveScene().buildIndex != 1 )
        {
            UpdateObjects();
        }
    }

    private void Update()
    {
       

       if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            if (enemySpawner != null && (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) && (enemySpawner.isWavesComplete()))
            {
                enemySpawner.SetWaveComplete(false);
                levelsComplete[SceneManager.GetActiveScene().buildIndex] = true;
                sceneLoader.ChangeToLevelSelect();

            }
            else if(GameObject.FindGameObjectsWithTag("Player").Length <= 0)
            {
                SceneManager.LoadScene(0);
            }
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
        
        if(FindObjectOfType<EnemySpawner>() != null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>().GetComponent<EnemySpawner>();
            sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(8);
        UpdateObjects();

    }

    public bool[] GetLevelsComplete()
    {
        return this.levelsComplete;
    }

    public void WaitAndUpdateObjects()
    {
        StartCoroutine(Wait());
    }
}
