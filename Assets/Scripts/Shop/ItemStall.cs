using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStall : MonoBehaviour
{
   [SerializeField] public UsableItem item;
   private SpriteRenderer spriteRenderer;

    private void Start(){
        spriteRenderer = getItemSpriteRenderer();
        spriteRenderer.sprite = item.buttonSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopKeeper shopKeeper = FindObjectOfType<ShopKeeper>();
        StartCoroutine(shopKeeper.PurchaseItem(item));
    }

    private SpriteRenderer getItemSpriteRenderer()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer spriteR in spriteRenderers)
        {
                // Is my ID (the parent) not the same as the component's gameObject
            if ( spriteR.gameObject.GetInstanceID() != gameObject.GetInstanceID() )
            {
                return spriteR;
            }
        }

        Debug.Log("ItemSpriteRenderer not found");
        return spriteRenderers[0];
    }

}
