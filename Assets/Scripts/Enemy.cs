﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, TakeDamageInterface
{

    [SerializeField] int maxHealth = 100;
    [SerializeField] int money = 1;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] LootTable dropTable = null;
    [SerializeField] GameObject coinsPrefab;
    ItemSpawner itemSpawner;

    private int health;
    private Material matFlash;
    private Material matDefault;
    private SpriteRenderer rendererReference;
    private bool damageFlashIsExecuting = false;
    private UnityEngine.Object deathSpatter;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        itemSpawner = FindObjectOfType<ItemSpawner>().GetComponent<ItemSpawner>();
        rendererReference = GetComponent<SpriteRenderer>();
        matFlash = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = rendererReference.material;
        deathSpatter = Resources.Load("Spatter");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine("Flash");

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

        GameObject onDeathEffect = (GameObject)Instantiate(deathSpatter);
        onDeathEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Instantiate(coinsPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public float GetMoveSpeed()
    {
        return this.moveSpeed;
    }

   private IEnumerator Flash()
    {
        if (damageFlashIsExecuting)
            yield break;

        damageFlashIsExecuting = true;
        rendererReference.material = matFlash;
        yield return new WaitForSeconds(0.1f);
        rendererReference.material = matDefault;
        damageFlashIsExecuting = false;
    }
}
