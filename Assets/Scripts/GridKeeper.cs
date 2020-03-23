using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridKeeper : MonoBehaviour
{

    private void Update()
    {
       if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameObject.SetActive(true);
        }
       else
        {
            gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        int gridCount = FindObjectsOfType<Grid>().Length;
        GameState gameState = FindObjectOfType<GameState>().GetComponent<GameState>();

        Debug.Log(gridCount);
        if (gridCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            gameState.SetLevelGrid(gameObject);
        }
    }
}
