using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int maxHealth = 100;
    [SerializeField] int money = 1;
    [SerializeField] float moveSpeed = 10f;
    //[SerializeField] int DropItemChance = 100;
    [SerializeField] LootTable dropTable = null;
    ItemSpawner itemSpawner;

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().IncrementMoneyCount(money);
        int dropElement = dropTable.ChooseDrop();
        if(dropElement != -1)
        {
            int dropType = dropTable.GetDropType(dropElement);
            ScriptableObject droppedItem = dropTable.GetDrop(dropElement);
            itemSpawner.SpawnUsableItemOrWeapon(transform.position, dropType, droppedItem);
        }
        Destroy(gameObject);
        
    }

    public float GetMoveSpeed()
    {
        return this.moveSpeed;
    }
}
