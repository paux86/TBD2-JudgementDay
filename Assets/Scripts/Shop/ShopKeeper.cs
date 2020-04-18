using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ShopKeeper : MonoBehaviour
{
    public PromptYesNo promptYesNo;
    private TextMeshProUGUI message;
    private PlayerStats playerStats;

    [TextArea(5,10)] public string defaultMessage;

    public void Start() 
    {
        message = gameObject.GetComponent<TextMeshProUGUI>();
        playerStats = FindObjectOfType<PlayerStats>();
        message.text = defaultMessage;
    }
    

    public IEnumerator PurchaseItem(UsableItem item)
    {
        message.text = "Do ye be wantin' to buy the " + item.itemName + "?\nIt'll costs ye " + item.cost + " o' yer Treasure";
        Debug.Log(message.text);

        Time.timeScale = 0; //Begin Prompt Player

        PromptYesNo prompt = Instantiate(promptYesNo, GameObject.FindGameObjectWithTag("Canvas").transform);
        while(!prompt.isResponded())
        {
            yield return null;
        }
        bool response = prompt.response;
        prompt.End();

        Time.timeScale = 1; //End Prompt Player

        if(response)
        {
            if(playerStats.GetMoneyCount() < item.cost)
            {
                message.text = "Sorry, ye dasn't be havin' enough treasure.";
                Debug.Log(message.text);

            }
            else
            {
                if(playerStats.UpdateItemSlot(item))
                {
                    playerStats.IncrementMoneyCount(-item.cost);
                    message.text = "Thank ye fer yer business!";
                    Debug.Log(message.text);
                }
                else
                {
                    message.text = "Sorry, yer inventory be full.";
                    Debug.Log(message.text);
                }
            }
        }
        else
        {
            message.text = "Then leave me be,\nYa horn swollgin' scallywag!";
            Debug.Log(message.text);
        }

        yield return new WaitForSeconds(5);
        message.text = defaultMessage;

        yield break;
    }

    public IEnumerator PurchaseWeapon(Weapon weapon)
    {
        message.text = "Do ye be wantin' to buy the " + weapon.name + "?\nIt'll costs ye " + weapon.cost + " o' yer Treasure";
        Debug.Log(message.text);

        Time.timeScale = 0; //Begin Prompt Player

        PromptYesNo prompt = Instantiate(promptYesNo, GameObject.FindGameObjectWithTag("Canvas").transform);
        while(!prompt.isResponded())
        {
            yield return null;
        }
        bool response = prompt.response;
        prompt.End();

        Time.timeScale = 1; //End Prompt Player

        if(response)
        {
            if(playerStats.GetMoneyCount() < weapon.cost)
            {
                message.text = "Sorry, ye dasn't be havin' enough treasure.";
                Debug.Log(message.text);

            }
            else
            {
                playerStats.UpdateWeaponSlot(weapon);
                playerStats.IncrementMoneyCount(-weapon.cost);
                message.text = "Thank ye fer yer business!";
                Debug.Log(message.text);
            }
        }
        else
        {
            message.text = "I doubt ye`ll last long,\nYa lily livered scurvy dog!";
            Debug.Log(message.text);
        }

        yield return new WaitForSeconds(5);
        message.text = defaultMessage;

        yield break;    }

    public IEnumerator ConfirmExitToMap()
    {
        message.text = "Are ye ready t' hade ou' again?";
        Debug.Log(message.text);

        Time.timeScale = 0; //Begin Prompt Player

        PromptYesNo prompt = Instantiate(promptYesNo, GameObject.FindGameObjectWithTag("Canvas").transform);
        while(!prompt.isResponded())
        {
            yield return null;
        }
        bool response = prompt.response;
        prompt.End();

        Time.timeScale = 1; //End Prompt Player

        if(response)
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            sceneLoader.ChangeToLevelSelect();
            // SceneManager.LoadScene("LevelSelect");
        }
        else
        {
            message.text = "Then get back here an' buy something";
        }

        yield return new WaitForSeconds(20);
        message.text = defaultMessage;

        yield break;    }
}
