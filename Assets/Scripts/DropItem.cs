using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject playerRef;
    [SerializeField] Vector2 offset;
    private ItemSpawner itemSpawner;
    private PlayerStats playerStatsRef;
    private Transform playerTransformRef;

    private void Start()
    {
        playerStatsRef = playerRef.GetComponent<PlayerStats>();
        playerTransformRef = playerRef.GetComponent<Transform>();
        itemSpawner = FindObjectOfType<ItemSpawner>();
    }

    public void DropInventoryItem(int slotIndex)
    {
        UsableItem itemToBeDropped = playerStatsRef.GetItemAtInventorySlot(slotIndex);
        if(itemToBeDropped != null)
        {
            //need to add a way to get this const from itemspawner
            int type = 2;
            Vector2 dropPos = new Vector2();
            dropPos.x = playerStatsRef.transform.position.x + offset.x;
            dropPos.y = playerStatsRef.transform.position.y + offset.y;
         
            itemSpawner.SpawnUsableItemOrWeapon(dropPos, type, itemToBeDropped);
            playerStatsRef.RemoveItemFromInventorySlot(slotIndex);

            Debug.Log("Dropped item at slot " + slotIndex);
        }
    }

    public void DropInventoryWeapon(int slotIndex)
    {
        //if equipped, swap and drop
    }
}
