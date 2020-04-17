using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStall : MonoBehaviour
{
   [SerializeField] public UsableItem item;

    private void Start(){
        gameObject.getComponent<Image>.sprite = item.buttonSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopKeeper shopKeeper = FindObjectOfType<shopKeeper>();
        shopKeeper.purchaseItem(item);
    }

}
