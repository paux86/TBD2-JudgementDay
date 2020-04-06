using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    RaycastHit2D hit;
    bool downClick;
    GameObject clickedButtonObject;
    SceneLoader sceneLoader;
    [SerializeField] string buttonPrefabName = "LevelTile(Clone)";
    int buttonLayer;



    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
        buttonLayer = 1 << LayerMask.NameToLayer("Button");

    }

    private void Update()
    {
        

        if(Input.GetMouseButtonDown(0))
        {
            downClick = true;
        }

        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10000,buttonLayer,0,10000);

       

        if (hit.collider != null && downClick)
        {

            if (hit.collider != null && Input.GetMouseButtonUp(0) && string.Equals(buttonPrefabName, hit.collider.gameObject.name))
            {
                clickedButtonObject = hit.collider.gameObject;
                NodeInformation clickednode = clickedButtonObject.GetComponent<NodeInformation>();
                if (clickednode != null)
                {
                    if(sceneLoader != null)
                    {
                        sceneLoader.ChangeSceneButton(clickednode);
                    }
                    else
                    {
                        Debug.LogError("sceneloader is nulll in button manager");
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                downClick = false;
            }
        }
    }


}
