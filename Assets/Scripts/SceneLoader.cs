using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;




public class SceneLoader : MonoBehaviour
{

    public void Start()
    {
        GetGameState();


    }

    GameState gameState;
    public string GetLevelNameFromButton()
    {
        string levelName = EventSystem.current.currentSelectedGameObject.name;
        int dot = levelName.LastIndexOf('.');
        dot++;
        levelName = levelName.Substring(dot);
       


        return levelName;
    }


    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ChangeSceneButton()
    {
        if (gameState != null)
        {
            gameState.WaitAndUpdateObjects();
        }
        SceneManager.LoadScene(GetLevelNameFromButton());
    }

    public void ChangeToLevelSelect()
    {
      
        SceneManager.LoadScene("LevelSelect");
    }


    private void GetGameState()
    {
        if (FindObjectOfType<GameState>() != null)
        {
            gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
        }
        else
        {
            Debug.Log("GameState object missing or not found within SceneManager");
        }
    }

}

  
