using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private float armor = 0.0f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBar = default;
    [SerializeField] public Weapon[] weaponInventory = new Weapon[3]; // 0 melee < 5 range, 1 medium < 50, 2 long > 50
    [SerializeField] public Weapon currentWeapon;
    [SerializeField] public UsableItem[] itemInventory = new UsableItem[3];

    private int health;
    private int currentWeaponSlot;
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
        if(number >= 0 && number < weaponInventory.Length && weaponInventory[number] != null)
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
            if(itemInventory[slotNum].IsConsumable)
            {
                itemInventory[slotNum] = null;
            }
            UpdateItemButton(slotNum);
        }
        else
        {
            Debug.Log("No item in slot " + slotNum);
        }
    }

    private void UpdateItemButton(int slotNum)
    {
        GameObject inventoryList = GameObject.Find("Item Inventory Buttons");
        Component[] invButtons;
        invButtons = inventoryList.GetComponentsInChildren(typeof(Button));


        InventoryHandler.UpdateItemButton(this,invButtons,slotNum);
    }

   

    public void UpdateWeaponSlot(Weapon weapon)
    {
        const int MELEE_SLOT = 0;
        const int MELEE_RANGE = 5;
        const int MED_SLOT = 1;
        const int MED_RANGE = 50;
        const int LONG_SLOT = 2;

        switch(weapon.range)
        {
            case float n when (n <= MELEE_RANGE):
                weaponInventory[MELEE_SLOT] = weapon;
                    break;
            case float n when (n <= MED_RANGE):
                weaponInventory[MED_SLOT] = weapon;
                break;
            case float n when (n > MED_RANGE):
                weaponInventory[LONG_SLOT] = weapon;
                break;
        }
    }

    public bool UpdateItemSlot(UsableItem usableItem)
    {
        bool foundSlot = false;

        for(int i = 0; i < itemInventory.Length && !foundSlot; i++)
        {
            if(itemInventory[i] == null)
            {
                itemInventory[i] = usableItem;
                foundSlot = true;
            }
        }

        return foundSlot;
    }

    public Sprite GetButtonSpriteForItemButton(int index, Component itemButton)
    {
        Sprite itemButtonSprite = null;

        if(itemInventory[index] != null)
        {
            if(itemInventory[index].buttonSprite != null)
            {
                itemButtonSprite = itemInventory[index].buttonSprite;
            }
            else
            {
                itemButton.GetComponentInChildren<Text>().text = "noSpr";
            }
        }

        return itemButtonSprite;
    }

    public Sprite GetButtonSpriteForWeaponButton(int index, Component weaponButton)
    {
        Sprite weaponButtonSprite = null;

        if(weaponInventory[index] != null)
        {
            if(weaponInventory[index].buttonSprite != null)
            {
                weaponButtonSprite = weaponInventory[index].buttonSprite;
            }
            else
            {
                weaponButton.GetComponentInChildren<Text>().text = "noSpr";
            }

        }
        return weaponButtonSprite;
    }


}
