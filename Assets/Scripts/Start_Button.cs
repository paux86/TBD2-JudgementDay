using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Button : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
