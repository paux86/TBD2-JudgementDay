using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, TakeDamageInterface
{
    [SerializeField] int maxHealth = 500;
    public bool isEnraged;
    public bool isInvulnerable = false;
    private int health;
    [SerializeField] LootTable dropTable = null;
    ItemSpawner itemSpawner;
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        isEnraged = false;
        gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
        itemSpawner = FindObjectOfType<ItemSpawner>().GetComponent<ItemSpawner>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (!isEnraged && health <= (maxHealth / 2))
        {
            isEnraged = true;
            GetComponent<Animator>().SetBool("Enraged", true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        int dropElement = dropTable.ChooseDrop();
        if(dropElement != -1)
        {
            int dropType = dropTable.GetDropType(dropElement);
            ScriptableObject droppedItem = dropTable.GetDrop(dropElement);
            itemSpawner.SpawnUsableItemOrWeapon(transform.position, dropType, droppedItem);
        }
        gameState.IncrementBossesDefeated();
        SpawnNewMapItem();
    }

    private void SpawnNewMapItem()
    {
        
        itemSpawner.SpawnExitToMapObject(true);
    }
}
