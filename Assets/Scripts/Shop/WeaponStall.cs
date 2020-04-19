using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStall : MonoBehaviour
{
    public Weapon weapon;
    private SpriteRenderer spriteRenderer;

    private void Start(){
        spriteRenderer = getItemSpriteRenderer();
        spriteRenderer.sprite = weapon.buttonSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopKeeper shopKeeper = FindObjectOfType<ShopKeeper>();
         StartCoroutine(shopKeeper.PurchaseWeapon(weapon));
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
