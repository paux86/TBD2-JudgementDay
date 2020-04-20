using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopKeeper shopKeeper = FindObjectOfType<ShopKeeper>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStats>().persistentStats.updateStats();
        StartCoroutine(shopKeeper.ConfirmExitToMap());
    }
}
