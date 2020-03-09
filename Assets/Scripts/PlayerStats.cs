using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private float armor = 0;

    public int getHealth()
    {
        return health;
    }

    public int getMoneyCount()
    {
        return moneyCount;
    }

    public float getArmor()
    {
        return armor;
    }

    public void incrementHealth(int number)
    {
        health += number;
    }

    public void incrementMoney(int number)
    {
        moneyCount += number;
    }

    public void setArmor(float number)
    {
        armor = number;
    }
}
