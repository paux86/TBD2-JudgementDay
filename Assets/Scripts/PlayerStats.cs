using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private float armor = 0;
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Weapon[] weaponInventory = new Weapon[3];
    public int health;

    void Start()
    {
        health = maxHealth;
        currentWeapon = weaponInventory[0];
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMoneyCount()
    {
        return moneyCount;
    }

    public float GetArmor()
    {
        return armor;
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void IncrementHealth(int number)
    {
        health += number;
    }

    public void IncrementMoney(int number)
    {
        moneyCount += number;
    }

    public void SetArmor(float number)
    {
        armor = number;
    }

    public void AddWeapon(Weapon newWeapon)
    {
        for(int i = 0; i < 3; i++)
        {
            if(currentWeapon == weaponInventory[i])
            {
                weaponInventory[i] = newWeapon;
                ChangeWeapon(i);
            }
        }
    }

    public void ChangeWeapon(int numOfWeapon)
    {
        currentWeapon = weaponInventory[numOfWeapon];
    }
}
