using UnityEngine;
using UnityEngine.SceneManagement;




public class SceneLoader : MonoBehaviour
{
    GameState gameState;

    public void Start()
    {
        GetGameState();


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


    public void ChangeSceneButton(NodeInformation selectedNode)
    {
        if (gameState != null)
        {
            gameState.WaitAndUpdateObjects();
        }


       if(selectedNode != null)
        {
            gameState.SetSelectedNode(selectedNode);
            SceneManager.LoadScene(selectedNode.GetScene());
        }
       else
        {
            Debug.Log("node passed to SceneLoader.ChangeSceneButton is null");
        }
        
    }

    public void ChangeToLevelSelect()
    {
      
        SceneManager.LoadScene("LevelSelect");
        if(gameState != null)
        {
            gameState.SetActiveLevelGrid(true);
        }
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

  
