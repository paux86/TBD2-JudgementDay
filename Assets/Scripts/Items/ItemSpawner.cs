﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
#pragma warning disable 0649
    //these are placeholders for whatever a loot table will contain
    [SerializeField] Weapon weapon;
    [SerializeField] UsableItem item;
    [SerializeField] ExitToMapObject mapObject;

    const int WEAPON_TYPE = 0;
    const int ITEM_TYPE = 1;
    const int POWERUP_TYPE = 2;
#pragma warning restore 0649
    public void SpawnUsableItemOrWeapon(Vector2 position, int type, ScriptableObject lootDrop)
    {
        GameObject itemObject = CreateDropObject();
        CircleCollider2D collider = itemObject.GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
        GenerateDropContainer(type, lootDrop, itemObject);

        if (itemObject != null && spriteRenderer != null)
        {
            //spriteRenderer.sprite = type == 0 ? weapon.sprite : item.sprite;
            switch(type)
            {
                case WEAPON_TYPE:
                    spriteRenderer.sprite = weapon.sprite;
                    break;
                case ITEM_TYPE:
                    spriteRenderer.sprite = item.sprite;
                    break;
                case POWERUP_TYPE:
                    spriteRenderer.sprite = item.powerupSprite;
                    break;
            }
        }
        else
        {
            Debug.LogError("Sprite for loot drop is null");
        }

        itemObject.transform.position = position;

    }

    private void GenerateDropContainer(int type, ScriptableObject lootDrop, GameObject itemObject)
    {
        if (type == WEAPON_TYPE)
        {
            this.weapon = (Weapon)lootDrop;
            DropContainer dropContainer = itemObject.GetComponent<DropContainer>();
            dropContainer.UpdateType(WEAPON_TYPE);
            dropContainer.SetWeapon(weapon);
        }
        else if (type == ITEM_TYPE)
        {
            this.item = (UsableItem)lootDrop;
            DropContainer dropContainer = itemObject.GetComponent<DropContainer>();
            dropContainer.UpdateType(ITEM_TYPE);
            dropContainer.SetItem(item);
        }
        else if (type == POWERUP_TYPE)
        {
            this.item = (UsableItem)lootDrop;
            DropContainer dropContainer = itemObject.GetComponent<DropContainer>();
            dropContainer.UpdateType(POWERUP_TYPE);
            dropContainer.SetItem(item);
            StartCoroutine(DelayCollider(dropContainer, 2f));
        }
    }

    private GameObject CreateDropObject()
    {
        GameObject itemObject = new GameObject();
        itemObject.AddComponent(typeof(SpriteRenderer));
        itemObject.AddComponent(typeof(CircleCollider2D));
        itemObject.AddComponent(typeof(DropContainer));
        itemObject.layer = LayerMask.NameToLayer("Item");
        itemObject.GetComponent<SpriteRenderer>().sortingLayerName = "Item";

        return itemObject;
    }

    public void SpawnExitToMapObject(bool isBoss)
    {
        ExitToMapObject mapObj = Instantiate(mapObject,Vector3.zero,Quaternion.identity );
        mapObj.SetIsBoss(isBoss);
    }

    private IEnumerator DelayCollider(DropContainer container, float delayInSeconds)
    {
        CircleCollider2D containerCollider = container.GetComponent<CircleCollider2D>();
        containerCollider.enabled = false;
        yield return new WaitForSeconds(delayInSeconds);
        containerCollider.enabled = true;
    }
}
