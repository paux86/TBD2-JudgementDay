using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Effect", menuName = "Item Effects/Item Effect")]

public abstract class UsableItemEffect : ScriptableObject
{
    public abstract void ExecuteEffect(UsableItem parentItem, PlayerStats playerstats);
}
