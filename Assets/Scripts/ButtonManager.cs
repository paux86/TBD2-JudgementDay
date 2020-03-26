using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    RaycastHit hit;
    Ray ray;
    bool downClick;
    GameObject clickedButtonObject;
    SceneLoader sceneLoader;
    [SerializeField] string ButtonPrefabName = "LevelTile(Clone)";



    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            downClick = true;
        }

        if (Physics.Raycast(ray, out hit) && downClick)
        {

         if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonUp(0) && string.Equals(ButtonPrefabName,hit.collider.gameObject.name) )
            {
                
                clickedButtonObject = hit.collider.gameObject;
                NodeInformation clickedNode = clickedButtonObject.GetComponent<NodeInformation>();
                if(clickedNode != null)
                {
                    sceneLoader.ChangeSceneButton(clickedNode);
                }
            }
         else if(Input.GetMouseButtonUp(0))
            {
                downClick = false;
            }
        }
    }


}
