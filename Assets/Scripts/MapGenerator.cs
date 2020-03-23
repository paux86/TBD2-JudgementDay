using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GridLayout grid;
    [SerializeField] GameObject levelButtonPrefab;
    [SerializeField] GameObject PathSpritePrefab;
    [SerializeField] GameObject foreground;
    [SerializeField] GameObject background;
    [SerializeField] GameState gameState;
    [SerializeField] int maxY = 3;
    //[SerializeField] int minY= -9;
    [SerializeField] int minX = -6;
    //[SerializeField] int maxX = 4;
    [SerializeField] int buttonSpacing = 3;
    [SerializeField] int levelCreationPercentage = 70;
    [SerializeField] int tiers = 5;
    [SerializeField] int maxLevelsPerTier = 4;

    


    



    // Start is called before the first frame update
    void Start()
    {
       if(!gameState.IsNodeTierMatrixInitialized())
        {
            GenerateLevelButtonsAndNodeMatrix(GenerateTierMatrix(tiers,maxLevelsPerTier));
        }

        DrawPointToPointPath(new Vector2(-6, -9), new Vector2(3, 3));

    }

    NodeInformation CreatePrefabInstanceAndNodeInfo(float xCoord, float yCoord)
    {
        GameObject instance = (GameObject)Instantiate(levelButtonPrefab);
        NodeInformation nodeInfo = instance.GetComponent<NodeInformation>();

        if (instance != null)
        {
            instance.transform.SetParent(foreground.transform);
            instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3(xCoord, yCoord, 0) + new Vector3(.5f, .5f, .5f)));
        }
        else
        {
            Debug.Log("Level Button Prefab is null when using MapGenerator");
        }

        return nodeInfo;
    }



    void GenerateLevelButtonsAndNodeMatrix(int[,] tierMatrix)
    {
        int y = maxY;
        int x = minX;
        NodeInformation currentNode;
        NodeInformation[,] nodeTierMatrix = new NodeInformation[tiers,maxLevelsPerTier];
        for(int i = 0; i < tierMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < tierMatrix.GetLength(1); j++)
            {
                if(tierMatrix[i,j] == 1)
                {
                    currentNode =  CreatePrefabInstanceAndNodeInfo(x, y);
                    currentNode.SetNodeId(i + "." + j);
                    currentNode.SetRowCol(i, j);
                    currentNode.SetNodePoint(new Vector2(x, y));
                    nodeTierMatrix[i, j] = currentNode;
                }
                
                x += buttonSpacing;
            }
            x = minX;
            y -= buttonSpacing;
        }
        gameState.SetNodeTierMatrix(nodeTierMatrix);

        
    }


    int[,] GenerateTierMatrix(int tiers, int maxLevelsPerTier)
    {
        int[,] tierMatrix = new int[tiers, maxLevelsPerTier];
        for (int i = 0; i < tiers; i++)
        {
            int guaranteedLevel = Random.Range(1, maxLevelsPerTier);
            for (int j = 0; j < maxLevelsPerTier; j++)
            {
                if (j == guaranteedLevel)
                {
                    tierMatrix[i, j] = 1;
                }
                else if (Random.Range(1, 100) < levelCreationPercentage)
                {
                    tierMatrix[i, j] = 1;
                }
            }
        }

        return tierMatrix;
    }

    void DrawPointToPointPath(Vector2 pointA, Vector2 pointB)
    {
        const int NUM_PATHS = 20;
        
        for (int i = 0; i <= NUM_PATHS; i++)
        {
            GameObject instance = (GameObject)Instantiate(PathSpritePrefab);
            instance.transform.SetParent(background.transform);
            instance.transform.position = Vector2.MoveTowards(pointA + new Vector2(0.5f,0.5f), pointB + new Vector2(0.5f, 0.5f), NUM_PATHS - i);
            
        }


    }



}
