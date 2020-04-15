using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private SceneLoader sceneLoader;

    private PersistentStats persistentStats;
    NodeInformation[,] nodeTierMatrix;
    GameObject levelGrid;
    NodeInformation currentSelectedNode;

    


    private void Start()
    {
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
                currentSelectedNode.SetIsComplete(true);
                persistentStats.updateStats();
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
            persistentStats = FindObjectOfType<PersistentStats>().GetComponent<PersistentStats>();

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(8);
        UpdateObjects();

    }


    public void WaitAndUpdateObjects()
    {
        StartCoroutine(Wait());
    }

    public bool IsNodeTierMatrixInitialized()
    {
        return nodeTierMatrix != null;
    }

    public NodeInformation[,] GetNodeTierMatrix()
    {
        return this.nodeTierMatrix;
    }

    public void SetNodeTierMatrix(NodeInformation[,] nodeTierMatrix)
    {
       this.nodeTierMatrix = nodeTierMatrix;
    }

    public void SetLevelGrid(GameObject levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    public GameObject GetLevelGrid()
    {
        return this.levelGrid;
    }

    public void SetActiveLevelGrid(bool active)
    {
       if(levelGrid != null)
        {
            this.levelGrid.SetActive(active);
        }
    }

    public void SetSelectedNode(NodeInformation selection)
    {
        if(selection != null)
        {
            this.currentSelectedNode = selection;

            UpdateTier(selection);
        }
        else
        {
            Debug.LogError("GameState.SetSelectNode input is null!");
        }
    }

    private void UpdateTier(NodeInformation selection)
    {
        if (nodeTierMatrix != null)
        {
            for (int i = 0; i < nodeTierMatrix.GetLength(1); i++)
            {
                if (nodeTierMatrix[selection.GetRow(), i] != null && i != selection.GetCol())
                {
                    nodeTierMatrix[selection.GetRow(), i].SetSelectable(false);
                }
            }
        }
        else
        {
            Debug.LogError("NodeTierMatrix in GameState is null");
        }
    }

    public void StartNewMap()
    {
        if (levelGrid != null)
        {
            nodeTierMatrix = null;
            currentSelectedNode = null;
            GridKeeper gridObj = levelGrid.GetComponent<GridKeeper>();
            gridObj.DestroyGrid();
            sceneLoader.ChangeToLevelSelect();
        }
        else
        {
            Debug.LogError("Level grid doesn't exist in GameState");
        }
    }

}
