using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathTransition : MonoBehaviour
{

    [SerializeField] int fadeOutRate = 100;

    private void Awake()
    {
     
        GameState gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        gameState.SetDeathTransition(this);
        gameObject.SetActive(false);
    }

    public void ActivateDeathTransition()
    {
        gameObject.SetActive(true);
        ActivateScreenAndButtons();
    }

    private void ActivateScreenAndButtons()
    {
        StartCoroutine(FadeInBackground());
    }

    IEnumerator FadeInBackground()
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeOutRate ;
            yield return null;
        }
        canvasGroup.interactable = true;
        
        yield return null;
    }

    
}
