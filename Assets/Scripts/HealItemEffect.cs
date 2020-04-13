using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Heal Effect", menuName = "Item Effects/Heal Effect")]
public class HealItemEffect : UsableItemEffect
{
    public int HealAmount;
    public override void ExecuteEffect(UsableItem parentItem, PlayerStats playerStats)
    {
        playerStats.IncrementHealth(HealAmount);
    }
}
