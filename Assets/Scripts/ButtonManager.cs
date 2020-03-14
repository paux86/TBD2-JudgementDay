using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    GameState gameState;
    [SerializeField] bool[] levelsComplete;
    [SerializeField] int sceneNum;
    [SerializeField] string sceneName;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        
        sceneName = gameObject.name;
        sceneNum = Int32.Parse(sceneName.Substring(0, 1));
        sceneName = sceneName.Substring(sceneName.LastIndexOf('.') + 1);
        button = gameObject.GetComponent<Button>();

   
        
        if(FindObjectOfType<GameState>() != null)
        {
            gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
            levelsComplete = gameState.GetLevelsComplete();

            switch (sceneNum)
            {
                case 1:
                    //do nothing
                    break;
                case 2:
                case 3:
                    if (levelsComplete[1] == false)
                    {
                        button.interactable = false;
                    }
                    break;
                case 4:
                    if (levelsComplete[1] == true && (levelsComplete[2] == true || levelsComplete[3] == true))
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }
                    break;
            }
        }
        else
        {
            Debug.Log("GameState object missing or not found within buttonmanager");
        }

        
    }

 
}
