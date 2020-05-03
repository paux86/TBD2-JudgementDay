using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Loot Table", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public List<ScriptableObject> Drops;
    public List<bool> IsWeapon;
    public List<int> Weights;
    public List<bool> Enabled;
    [SerializeField] private int totalWeight = 0;
    private int rolledValue = -1;
    ItemSpawner itemSpawner;

    public int ChooseDrop()
    {
        rolledValue = UnityEngine.Random.Range(0, totalWeight);
        for(int i=0;i<Weights.Count;i++)
        {
            if(rolledValue <= Weights[i])
            {
                if(Enabled[i] && rolledValue > -1)
                {
                    return i;
                }
                else
                {   
                    return -1;
                }
            }
            else if(rolledValue > Weights[i])
            {
                rolledValue -= Weights[i];
            }
        }
        return -1;
    }

    public int GetDropType(int dropElement)
    {
        if(Enabled[dropElement])
        {
            if(IsWeapon[dropElement])
                return 0;
            else
                return 1;
        }
        else
            return -1;
    }

    public ScriptableObject GetDrop(int dropElement)
    {
        return Drops[dropElement];
    }
}
