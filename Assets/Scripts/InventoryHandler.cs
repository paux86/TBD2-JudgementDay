using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject[] inventoryLists;
    [SerializeField] GameObject playerReference;
#pragma warning restore 0649

    private bool isOpen = false;


    public void ToggleInventory()
    {
        LoadButtons();
        isOpen = !isOpen;

        if (inventoryLists.Length > 0)
        {
            foreach (GameObject  list in inventoryLists)
            {
                list.SetActive(isOpen);
            }
        }

        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    private void LoadButtons()
    {
        //init weapons
        Component[] wepButtons;
        wepButtons = inventoryLists[0].GetComponentsInChildren(typeof(Button));
        SwapWeapon swapReference = playerReference.GetComponent<SwapWeapon>();
        PlayerStats playerStatsReference = playerReference.GetComponent<PlayerStats>();

        if (wepButtons != null)
        {
            for (int i = 0; i < wepButtons.Length; i++)
            {
                int b = i;
                //wepButtons[i].GetComponentInChildren<Text>().text = "Wep" + i;
                wepButtons[i].GetComponent<Button>().onClick.AddListener(() => swapReference.Swap(b));
                UpdateWepButton(wepButtons, playerStatsReference, i);

            }
        }

        //init items
        Component[] itemButtons;
        itemButtons = inventoryLists[1].GetComponentsInChildren(typeof(Button));
        

        if (itemButtons != null)
        {
            for (int i = 0; i < itemButtons.Length; i++)
            {
                int b = i;
                //itemButtons[i].GetComponentInChildren<Text>().text = "Item" + i;
                itemButtons[i].GetComponent<Button>().onClick.AddListener(() => playerStatsReference.UseItem(b));
                UpdateItemButton(playerStatsReference, itemButtons, i);

            }
        }
    }

    public static void UpdateItemButton(PlayerStats playerStatsReference, Component[] itemButtons, int i)
    {
        Sprite itemButton = playerStatsReference.GetButtonSpriteForItemButton(i, itemButtons[i]);
        if (itemButton != null || itemButtons[i].GetComponentInChildren<Text>().text.Equals("noSpr"))
        {
            itemButtons[i].GetComponent<Image>().sprite = itemButton;
            itemButtons[i].GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        }
        else
        {
            itemButtons[i].GetComponent<Image>().sprite = null;
            itemButtons[i].GetComponent<Image>().color = new Color(0f, 0f, 0f, .5f);
        }
    }

    private static void UpdateWepButton(Component[] wepButtons, PlayerStats playerStatsReference, int i)
    {
        Sprite wepButton = playerStatsReference.GetButtonSpriteForWeaponButton(i,wepButtons[i]);
        if (wepButton != null || wepButtons[i].GetComponentInChildren<Text>().text.Equals("noSpr"))
        {
            wepButtons[i].GetComponent<Image>().sprite = wepButton;
            wepButtons[i].GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        }
        else
        {
            wepButtons[i].GetComponent<Image>().sprite = null;
            wepButtons[i].GetComponent<Image>().color = new Color(0f, 0f, 0f, .5f);
        }
    }
}
