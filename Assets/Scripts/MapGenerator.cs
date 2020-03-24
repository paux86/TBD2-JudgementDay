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
    NodeInformation[,] nodeTierMatrix;








    // Start is called before the first frame update
    void Start()
    {
       if(!gameState.IsNodeTierMatrixInitialized())
        {
            GenerateLevelButtonsAndNodeMatrix(GenerateTierMatrix(tiers,maxLevelsPerTier));
            CreateNodeConnections();
        }

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
        nodeTierMatrix = new NodeInformation[tiers,maxLevelsPerTier];
        for(int i = 0; i < tierMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < tierMatrix.GetLength(1); j++)
            {
                NodeInformation currentNode;
                if (tierMatrix[i,j] == 1)
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
        const int NUM_PATHS = 100;
        Vector2 currentPos = pointA + new Vector2(0.5f, 0.5f);
        
        for (int i = 0; i <= NUM_PATHS; i++)
        {
            GameObject instance = (GameObject)Instantiate(PathSpritePrefab);
            instance.transform.SetParent(background.transform);
            instance.transform.position = Vector2.MoveTowards(currentPos, pointB + new Vector2(0.5f, 0.5f), 0.1f);
            instance.transform.Rotate(new Vector3(0, 0, Random.Range(0, 359)));
            currentPos = instance.transform.position;
            
        }


    }

    void CreateNodeConnections()
    {
        bool zeroFlag = true;
        
        for(int i = nodeTierMatrix.GetLength(0) -1; i >= 1; i--)
        {
            for(int j = 0; j < nodeTierMatrix.GetLength(1); j++)
            {
                if(nodeTierMatrix[i,j] != null)
                {
                  if(nodeTierMatrix[(i-1),j] != null)
                    {
                        nodeTierMatrix[i, j].AddNextNode(nodeTierMatrix[(i - 1), j]);
                        nodeTierMatrix[(i - 1), j].AddPreviousNode(nodeTierMatrix[i, j]);
                        DrawPointToPointPath(nodeTierMatrix[i, j].GetNodePoint(), nodeTierMatrix[(i - 1), j].GetNodePoint());
                    }

                    for (int k = j + 1; k < nodeTierMatrix.GetLength(1) && zeroFlag; k++)
                    {

                        if (nodeTierMatrix[(i - 1), k] != null && (nodeTierMatrix[i,k] == null || nodeTierMatrix[i,j].GetNumNextNodes() == 0))
                        {

                            nodeTierMatrix[i, j].AddNextNode(nodeTierMatrix[(i - 1), k]);
                            nodeTierMatrix[(i - 1), k].AddPreviousNode(nodeTierMatrix[i, j]);
                            DrawPointToPointPath(nodeTierMatrix[i, j].GetNodePoint(), nodeTierMatrix[(i - 1), k].GetNodePoint());

                            if(nodeTierMatrix[i, k] != null && nodeTierMatrix[i, j].GetNumNextNodes() > 0)
                            {
                                zeroFlag = false;
                            }
                        }
                        else if (nodeTierMatrix[(i - 1), k] != null && nodeTierMatrix[i, k] != null)
                        {

                            zeroFlag = false;
                        }

                    }
                    zeroFlag = true;
                }
            }
        }

        
        int closeCol = nodeTierMatrix.GetLength(1) + 1;
        zeroFlag = true;
        for (int i = nodeTierMatrix.GetLength(0) - 1; i >= 0; i--)
        {
            for(int j = nodeTierMatrix.GetLength(1) - 1; j >= 0; j--)
            {
                if(nodeTierMatrix[i,j] != null)
                {
                    if (nodeTierMatrix[i, j].GetNumNextNodes() == 0 && i > 0)
                    {
                        for (int k = nodeTierMatrix.GetLength(1) - 1; k >= 0 && zeroFlag; k--)
                        {
                            if (nodeTierMatrix[(i - 1), k] != null)
                            {
                                nodeTierMatrix[i, j].AddNextNode(nodeTierMatrix[(i - 1), k]);
                                nodeTierMatrix[(i - 1), k].AddPreviousNode(nodeTierMatrix[i, j]);
                                DrawPointToPointPath(nodeTierMatrix[i, j].GetNodePoint(), nodeTierMatrix[(i - 1), k].GetNodePoint());
                                zeroFlag = false;
                            }
                        }
                        zeroFlag = true;
                       
                    }
                    if (nodeTierMatrix[i, j].GetNumPreviousNodes() == 0 && i < (nodeTierMatrix.GetLength(0) - 1))
                    {
                        
                        closeCol = nodeTierMatrix.GetLength(1) +1;
                        for (int k = nodeTierMatrix.GetLength(1) - 1; k >= 0; k--)
                        {
                            if (nodeTierMatrix[(i + 1), k] != null)
                            {
                                if(Mathf.Abs(j - k) < Mathf.Abs(j - closeCol))
                                {
                                    closeCol = k;
                                }
                            }
                        }
                        nodeTierMatrix[i, j].AddPreviousNode(nodeTierMatrix[(i + 1), closeCol]);
                        nodeTierMatrix[(i + 1), closeCol].AddNextNode(nodeTierMatrix[i, j]);
                        DrawPointToPointPath(nodeTierMatrix[i, j].GetNodePoint(), nodeTierMatrix[(i + 1), closeCol].GetNodePoint());
                        

                        
                    }
                }
            }
        }

    }



}
