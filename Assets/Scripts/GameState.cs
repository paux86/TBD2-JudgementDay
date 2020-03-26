﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private SceneLoader sceneLoader;
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
        this.currentSelectedNode = selection;

        UpdateTier(selection);
    }

    private void UpdateTier(NodeInformation selection)
    {
        for (int i = 0; i < nodeTierMatrix.GetLength(1); i++)
        {
            if (i != selection.GetCol() && nodeTierMatrix[selection.GetRow(),i] != null)
            {
                nodeTierMatrix[selection.GetRow(), i].SetSelectable(false);
            }
        }
    }

}
