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


        return levelName;
    }

    public void ChangeSceneButton()
    {

        SceneManager.LoadScene(GetLevelNameFromButton());
    }

    public void ChangeToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
