using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int moneyCount = 0;
    private PlayerStats playerStats;

    public void updateStats(){
        playerStats = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
        this.moneyCount = playerStats.GetMoneyCount();
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
}
