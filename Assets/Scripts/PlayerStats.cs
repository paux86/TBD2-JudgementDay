﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private float armor = 0.0f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBar = default;
    [SerializeField] public Weapon[] weaponInventory = new Weapon[3];
    [SerializeField] public Weapon currentWeapon;
    [SerializeField] public UsableItem[] itemInventory = new UsableItem[3];

    private int health;
    private int currentWeaponSlot;
    private int numberOfEquippedWeapons = 0;
    private int weaponInventorySize;

    private PersistentStats persistentStats;

    private void Start()
    {

        persistentStats = FindObjectOfType<PersistentStats>().GetComponent<PersistentStats>();
        this.moneyCount = persistentStats.moneyCount;
        this.weaponInventory = persistentStats.GetWeaponInventory();
        this.itemInventory = persistentStats.GetItemInventory();


        health = maxHealth;
        if(healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        //weaponInventory = new Weapon[weaponInventorySize];
        currentWeapon = weaponInventory[0];
        currentWeaponSlot = 0;
        weaponInventorySize = weaponInventory.Length;
        for(int i = 0; i < weaponInventorySize; i++)
        {
            if (weaponInventory[i] != null)
                numberOfEquippedWeapons++;
        }

        armor = GetArmor();
    }

    public int GetMoneyCount()
    {
        return moneyCount;
    }

    public void IncrementMoneyCount(int number)
    {
        moneyCount += number;
    }

    public float GetArmor()
    {
        return armor;
    }

    public void SetArmor(float number)
    {
        armor = number;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void IncrementHealth(int number)
    {
        health += number;
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void SwapToNextWeapon()
    {
        currentWeaponSlot = (currentWeaponSlot + 1) % weaponInventorySize;
        currentWeapon = weaponInventory[currentWeaponSlot];
    }

    public void ChangeCurrentWeapon(int number)
    {
        if(number >= 0 && number < numberOfEquippedWeapons)
        {
            currentWeapon = weaponInventory[number];
            currentWeaponSlot = number;
        }
        else
        {
            Debug.Log("No weapon in slot" + number);
        }
    }

    public void AddWeaponToInventory(Weapon newWeapon)
    {
        if(weaponInventory.Length == 3)
        {
            for(int i = 0;i < 3; i++)
            {
                if(currentWeapon == weaponInventory[i])
                {
                    weaponInventory[i] = newWeapon;
                    ChangeCurrentWeapon(i);
                }
            }
        }
        else
        {
            weaponInventory[weaponInventory.Length] = newWeapon;
        }
    }

    public Weapon[] GetWeaponInventory(){
        Weapon[] returnWeaponInventory = new Weapon[weaponInventory.Length];
        weaponInventory.CopyTo(returnWeaponInventory, 0);
        return returnWeaponInventory;
    }

    public UsableItem[] GetItemInventory(){
        UsableItem[] returnItemInventory = new UsableItem[itemInventory.Length];
        itemInventory.CopyTo(returnItemInventory, 0);
        return returnItemInventory;
    }

    public void TakeDamage(int attackDamage)
    {
        health -= attackDamage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void UseItem(int slotNum)
    {
        if(itemInventory[slotNum] != null)
        {
            itemInventory[slotNum].Use(this);
        }
        else
        {
            Debug.Log("No item in slot " + slotNum);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with this item. Will delte it now. Please code an option to add me to player inventory later");
        Destroy(collision.gameObject);
    }
}
