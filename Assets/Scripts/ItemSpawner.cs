using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
#pragma warning disable 0649
    //these are placeholders for whatever a loot table will contain
    [SerializeField] Weapon weapon;
    [SerializeField] UsableItem item;

    const int WEAPON_TYPE = 0;
    const int ITEM_TYPE = 1;
#pragma warning restore 0649
    public void SpawnUsableItemOrWeapon(Vector2 position,int type)
    {
        GameObject itemObject = CreateDropObject();
        CircleCollider2D collider = itemObject.GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 0;
        GenerateDropContainer(type, itemObject);

        if (itemObject != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = type == 0 ? weapon.sprite : item.sprite;
        }
        else
        {
            Debug.LogError("Sprite for item in itemspawner is null");
        }

        itemObject.transform.position = position;

    }

    private void GenerateDropContainer(int type, GameObject itemObject)
    {
        if (type == WEAPON_TYPE)
        {
            DropContainer dropContainer = itemObject.GetComponent<DropContainer>();
            dropContainer.UpdateType(WEAPON_TYPE);
            dropContainer.SetWeapon(weapon);
        }
        else if (type == ITEM_TYPE)
        {
            DropContainer dropContainer = itemObject.GetComponent<DropContainer>();
            dropContainer.UpdateType(ITEM_TYPE);
            dropContainer.SetItem(item);
        }
    }

    private GameObject CreateDropObject()
    {
        GameObject itemObject = new GameObject();
        itemObject.AddComponent(typeof(SpriteRenderer));
        itemObject.AddComponent(typeof(CircleCollider2D));
        itemObject.AddComponent(typeof(DropContainer));
        itemObject.layer = LayerMask.NameToLayer("Item");

        return itemObject;
    }

    

    
}
