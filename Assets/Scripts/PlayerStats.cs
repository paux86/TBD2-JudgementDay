using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private float armor = 0;
    [SerializeField] private Weapon weapon;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        SetHealth(maxHealth);
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

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public void SetHealth(int number)
    {
        health = number;
    }

    public void SetMoney(int number)
    {
        moneyCount = number;
    }

    public void SetArmor(float number)
    {
        armor = number;
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
}
