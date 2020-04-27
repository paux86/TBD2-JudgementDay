using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour,TakeDamageInterface
{
    [SerializeField] int maxHealth = 500;
    private int health;
    [SerializeField] LootTable dropTable = null;
    ItemSpawner itemSpawner;
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
        itemSpawner = FindObjectOfType<ItemSpawner>().GetComponent<ItemSpawner>();
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("BossDeath");
        animator.tag = "Untargetable";
        int dropElement = dropTable.ChooseDrop();
        if(dropElement != -1)
        {
            int dropType = dropTable.GetDropType(dropElement);
            ScriptableObject droppedItem = dropTable.GetDrop(dropElement);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            itemSpawner.SpawnUsableItemOrWeapon(player.transform.position, dropType, droppedItem);
        }
        gameState.IncrementBossesDefeated();
        SpawnNewMapItem();
    }

    private void SpawnNewMapItem()
    {
        itemSpawner.SpawnExitToMapObject(true);
    }
}
