using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptYesNo : MonoBehaviour
{

    public bool response;
    [SerializeField] private bool responded;

    public GameObject yesButton;
    public GameObject noButton;

    private GameObject yes;
    private GameObject no;

    public void Start()
    {
        Debug.Log("Start Prompt");
        responded = false;
        // yes = Instantiate(yesButton);
        // yes.transform.SetParent(gameObject.transform, false);
        // no = Instantiate(noButton);
        // no.transform.SetParent(gameObject.transform, false);

    }

    public void End()
    {
        Debug.Log("End Prompt.");
        // Destroy(yes);
        // Destroy(no);
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }

    public bool isResponded()
    {
        bool retVal = responded;
        Debug.Log("responded: " + responded);
        return retVal;
    }

    public void respondYes()
    {
        Debug.Log("Respond Yes.");
        response = true;
        responded = true;
    }

    public void respondNo()
    {
        Debug.Log("Respond No.");
        response = false;
        responded = true;
    }
}
