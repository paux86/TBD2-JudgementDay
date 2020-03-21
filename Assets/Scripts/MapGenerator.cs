using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GridLayout grid;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject foreground;
    const int MAX_Y = 8;
    const int MIN_Y= -9;
    const int MIN_X = -5;
    const int MAX_X = 4;



    // Start is called before the first frame update
    void Start()
    {

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        if(instance != null)
        {
            instance.transform.SetParent(foreground.transform);
            instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3(5, 8, 0) + new Vector3(.5f, .5f, .5f)));
        }
        
    }

   
}
