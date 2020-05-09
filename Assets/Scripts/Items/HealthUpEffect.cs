using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New HealthUp Effect", menuName = "Item Effects/HealthUp Effect")]
public class HealthUpEffect : UsableItemEffect
{
    public int HealthAmount;
    public override void ExecuteEffect(UsableItem parentItem, PlayerStats playerStats)
    {
        playerStats.IncrementHealthMax(HealthAmount);
    }
}
