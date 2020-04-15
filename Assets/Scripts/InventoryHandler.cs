using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] GameObject[] inventoryLists;
    [SerializeField] GameObject playerReference;

    private bool isOpen = false;

    private void Start()
    {
        InitializeButtons();
    }

    public void ToggleInventory()
    {
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

    private void InitializeButtons()
    {
        SwapWeapon swapReference = playerReference.GetComponent<SwapWeapon>();

        foreach(GameObject list in inventoryLists)
        {
            Component[] buttons;
            buttons = list.GetComponentsInChildren(typeof(Button));

            if(buttons != null)
            {
                for(int i = 0; i < buttons.Length; i++)
                {
                    int b = i;
                    buttons[i].GetComponentInChildren<Text>().text = "" + i;
                    buttons[i].GetComponent<Button>().onClick.AddListener(() => swapReference.Swap(b));
                }
            }
        }
    }
}
