using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitToMapObject : MonoBehaviour
{

#pragma warning disable 0649
    [SerializeField] PromptYesNo promptYesNo;
    [SerializeField] [TextArea(5, 10)] string defaultMessage;
    [SerializeField] GameObject mapNotification;
#pragma warning restore 0649

    TextMeshProUGUI notification;
    GameObject mapNotty;
    bool isBoss = false;
    GameState gameState;



    private void Start()
    {
        gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
        mapNotty = Instantiate(mapNotification, GameObject.FindGameObjectWithTag("Canvas").transform);
        notification = mapNotty.GetComponentInChildren<TextMeshProUGUI>();
        mapNotty.transform.position = new Vector3(280, 50, 0);
        RectTransform rect = mapNotty.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 0);
        mapNotty.transform.localScale = new Vector3(4.5f, 1, 0);
        rect.anchoredPosition = new Vector2(-90, 53);
        if (notification != null)
        {
            notification.text = defaultMessage;
            
        }
        else
        {
            Debug.Log("MapNotification prefab not initialized in inspector");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(ConfirmExitToMap());
    }

    public IEnumerator ConfirmExitToMap()
    {
        this.notification.text = "Are ye ready t' hade ou' again?";

        Time.timeScale = 0; //Begin Prompt Player

        PromptYesNo prompt = Instantiate(promptYesNo, GameObject.FindGameObjectWithTag("Canvas").transform);
        Vector3 oldPosition = mapNotty.transform.position;
        mapNotty.transform.localScale = new Vector3(6.5f, 1.5f, 0);
        mapNotty.transform.position = prompt.transform.position;
        mapNotty.transform.position = new Vector3(prompt.transform.position.x, prompt.transform.position.y + 200, prompt.transform.position.z);
        while (!prompt.isResponded())
        {
            yield return null;
        }
        bool response = prompt.response;
        prompt.End();

        Time.timeScale = 1; //End Prompt Player

        if (response)
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            PersistentStats persistentStats = FindObjectOfType<GameState>().GetComponent<GameState>().GetPersistantStats();
            persistentStats.updateStats();
            if(!isBoss)
            {
                sceneLoader.ChangeToLevelSelect();
            }
            else
            {
                gameState.StartNewMap();
            }
        }
        else
        {
            mapNotty.transform.localScale = new Vector3(4.5f, 1, 0);
            mapNotty.transform.position = oldPosition;
            this.notification.text = defaultMessage;
        }

        yield return new WaitForSeconds(20);
        this.notification.text = defaultMessage;

        yield break;
    }

    public void SetIsBoss(bool isBoss)
    {
        this.isBoss = isBoss;
    }
}
