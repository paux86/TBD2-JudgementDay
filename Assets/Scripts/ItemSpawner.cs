using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] Weapon weapon;
    [SerializeField] UsableItem item;
    public void SpawnUsableItemOrWeapon(Vector2 position,int type)
    {
        GameObject itemObject = new GameObject();
        itemObject.AddComponent(typeof(SpriteRenderer));
        itemObject.AddComponent(typeof(CircleCollider2D));
        itemObject.layer =LayerMask.NameToLayer("Item");
        CircleCollider2D collider = itemObject.GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 0;

        if(itemObject != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = type == 0 ? weapon.sprite : item.sprite;
        }
        else
        {
            Debug.LogError("Sprite for item in itemspawner is null");
        }

        itemObject.transform.position = position;
        
    }

    
}
