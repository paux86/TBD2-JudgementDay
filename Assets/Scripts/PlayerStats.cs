using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int health;
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private float armor = 0;

    // Start is called before the first frame update
    void Start()
    {
        setHealth(maxHealth);
    }

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

    public void setHealth(int number)
    {
        health = number;
    }

    public void setMoney(int number)
    {
        moneyCount = number;
    }

    public void setArmor(float number)
    {
        armor = number;
    }
}

}
