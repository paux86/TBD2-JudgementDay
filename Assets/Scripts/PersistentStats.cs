using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int moneyCount = 0;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentWeaponSlot;


    //These arrays are private and accessed by the getter functions to ensure playerStats gets a copy of the arrays not a reference to them
    [SerializeField] private Weapon[] weaponInventory = new Weapon[3];
    [SerializeField] private UsableItem[] itemInventory = new UsableItem[3];

    private PlayerStats playerStats;

    public void updateStats(){
        playerStats = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
        this.moneyCount = playerStats.GetMoneyCount();
        this.maxHealth = playerStats.GetMaxHealth();
        this.currentWeaponSlot = playerStats.GetCurrentWeaponSlot();

        this.weaponInventory = playerStats.GetWeaponInventory();
        this.itemInventory = playerStats.GetItemInventory();

    }
        private void Awake()
    {
        int persistentStatsCount = FindObjectsOfType<PersistentStats>().Length;
        if (persistentStatsCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
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
}
