using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public GameObject[] inventoryLists;
    private bool isOpen = false;

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
}
