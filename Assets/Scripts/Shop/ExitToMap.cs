using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopKeeper shopKeeper = FindObjectOfType<ShopKeeper>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameState gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
        NodeInformation currentNode = gameState.GetCurrentSelectedNode();
        PersistentStats persistent = FindObjectOfType<PersistentStats>().GetComponent<PersistentStats>();
        persistent.updateStats();
        currentNode.SetIsComplete(true);
        StartCoroutine(shopKeeper.ConfirmExitToMap());
    }
}
