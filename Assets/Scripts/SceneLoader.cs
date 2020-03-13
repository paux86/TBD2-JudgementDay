using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string GetLevelNameFromButton()
    {
        string levelName = EventSystem.current.currentSelectedGameObject.name;

        Debug.Log(levelName);


        return levelName;
    }

    public void ChangeScene()
    {

        SceneManager.LoadScene(GetLevelNameFromButton());
    }
}
