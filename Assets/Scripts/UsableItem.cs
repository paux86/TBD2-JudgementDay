using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Usable Item", menuName = "Usable Item")]
public class UsableItem : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite sprite;
    public bool IsConsumable;
    public List<UsableItemEffect> Effects;

    public virtual void Use(PlayerStats platerStats)
    {
        foreach (UsableItemEffect effect in Effects)
        {
            effect.ExecuteEffect(this, platerStats);
        }
    }
}
