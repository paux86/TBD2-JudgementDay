using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private float armor = 0.0f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBar = default;
    [SerializeField] Weapon[] weaponInventory = new Weapon[3];
    [SerializeField] public Weapon currentWeapon;

    private int health;
    private int currentWeaponSlot;
    private int numberOfEquippedWeapons = 0;
    private int weaponInventorySize;

    private void Start()
    {
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
        moneyCount = GetMoneyCount();
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
        Debug.Log(number);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with this item. Will delte it now. Please code an option to add me to player inventory later");
        Destroy(collision.gameObject);
    }
}
