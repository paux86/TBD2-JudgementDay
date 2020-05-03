using UnityEngine;

public class NodeInformation : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Sprite bossLevelSprite;
    [SerializeField] Sprite shopSceneSprite;
    [SerializeField] Sprite completedLevelSprite;
    [SerializeField] Sprite bossLevel2Sprite;
#pragma warning restore 0649
    string nodeId;
    NodeInformation[] nextNodes = new NodeInformation[5];
    NodeInformation[] previousNodes = new NodeInformation[5];
    int numNextNode;
    int numPreviousNode;
    int row;
    int col;
    Vector2 nodePoint;
    bool selectable;
    bool isComplete;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    int sceneBuildIndex = 2;
    GameState gameState;



    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
        SetupNode();
        UpdateNode();
    }

    private void SetupNode()
    {
        if (string.Equals(nodeId, "Boss Node"))
        {
            SetupBossNodes();
        }
        else if (string.Equals(nodeId, "Shop Scene"))
        {
            sceneBuildIndex = 6;
            selectable = true;
            if(spriteRenderer != null)
            {
                if(shopSceneSprite != null)
                {
                    spriteRenderer.sprite = shopSceneSprite;
                }
                else
                {
                    Debug.Log("shopSceneSprite not set in nodeinformation inspector (most likely button prefab)");
                }
                gameObject.transform.localScale = new Vector3(1, 1, 0);
            }
        }
        else
        {
            sceneBuildIndex = Random.Range(2, 5);
        }
    }

    private void SetupBossNodes()
    {
        if (gameState.GetBossesDefeated() == 0)
        {
            SetupFirstBossNode();
            gameObject.transform.localScale = new Vector3(4, 4, 0);

        }
        else
        {
            SetupSecondBossNode();
            gameObject.transform.localScale = new Vector3(1, 1, 0);
        }
        
    }

    private void SetupSecondBossNode()
    {
        sceneBuildIndex = 7;
        if (bossLevel2Sprite != null)
        {
            spriteRenderer.sprite = bossLevel2Sprite;
        }
        else
        {
            Debug.Log("bossLevel2Sprite not set in nodeinformation inspector (most likely button prefab)");
        }
    }

    private void SetupFirstBossNode()
    {
        sceneBuildIndex = 5;
        if (bossLevelSprite != null)
        {
            spriteRenderer.sprite = bossLevelSprite;
        }
        else
        {
            Debug.Log("bossLevelSprite not set in nodeinformation inspector (most likely button prefab)");
        }
    }

    public void SetNodeId(string nodeId)
    {
        this.nodeId = nodeId;
    }

    public string GetNodeId()
    {
        return this.nodeId;
    }

    public int GetRow()
    {
        return this.row;
    }

    public int GetCol()
    {
        return this.col;
    }

    public void SetRowCol(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetNodePoint(Vector2 point)
    {
        this.nodePoint = point;
    }

    public Vector2 GetNodePoint()
    {
        return this.nodePoint;
    }

    public void AddNextNode(NodeInformation node)
    {
        nextNodes[numNextNode] = node;
        numNextNode++;
    }

    public void AddPreviousNode(NodeInformation node)
    {
        previousNodes[numPreviousNode] = node;
        numPreviousNode++;
    }

    public int GetNumPreviousNodes()
    {
        return this.numPreviousNode;
    }

    public int GetNumNextNodes()
    {
        return this.numNextNode;
    }

    public void SetSelectable(bool isSelectable)
    {
        this.selectable = isSelectable;
        UpdateNode();
    }

    void UpdateNode()
    {

        if(selectable && isComplete)
        {
            DeactivateNode();
            MakeNextNodesSelectable();
            if(completedLevelSprite != null)
            {
                spriteRenderer.sprite = completedLevelSprite;
            }
            else
            {
                Debug.Log("Completed level sprite not set in Node information inspector. (most likely button level prefab)");
            }
        }
        else if(selectable)
        {
            ActivateNode();
        }
        else
        {
            DeactivateNode();
        }

    }

    private void MakeNextNodesSelectable()
    {
        for (int i = 0; i < nextNodes.GetLength(0); i++)
        {
            if (nextNodes[i] != null)
            {
                nextNodes[i].SetSelectable(true);
            }
        }
    }

    private void ActivateNode()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
    }

    private void DeactivateNode()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }

    public int GetScene()
    {
        return this.sceneBuildIndex;
    }


    public bool IsComplete()
    {
        return this.isComplete;
    }

    public void SetIsComplete(bool isComplete)
    {
        this.isComplete = isComplete;
        UpdateNode();
    }

    


}
