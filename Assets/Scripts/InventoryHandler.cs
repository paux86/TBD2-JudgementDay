using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] GameObject[] inventoryLists;
    [SerializeField] GameObject playerReference;

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

        if (wepButtons != null)
        {
            for (int i = 0; i < wepButtons.Length; i++)
            {
                int b = i;
                wepButtons[i].GetComponentInChildren<Text>().text = "Wep" + i;
                wepButtons[i].GetComponent<Button>().onClick.AddListener(() => swapReference.Swap(b));
            }
        }

        //init items
        Component[] itemButtons;
        itemButtons = inventoryLists[1].GetComponentsInChildren(typeof(Button));
        PlayerStats playerStatsReference = playerReference.GetComponent<PlayerStats>();

        if (itemButtons != null)
        {
            for (int i = 0; i < itemButtons.Length; i++)
            {
                int b = i;
                itemButtons[i].GetComponentInChildren<Text>().text = "Item" + i;
                itemButtons[i].GetComponent<Button>().onClick.AddListener(() => playerStatsReference.UseItem(b));
            }
        }
    }
}
