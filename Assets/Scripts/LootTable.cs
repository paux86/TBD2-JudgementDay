using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Loot Table", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public List<GameObject> Drops;
    public List<int> Weights;
    public List<bool> Enabled;
    public int totalWeight = 0;
    private int rolledValue = 0;

    public int CalculateTotalWeight()
    {
        foreach(int weight in Weights)
        {
            totalWeight += weight;
        }

        return totalWeight;
    }

    public int RollTable()
    {
        return UnityEngine.Random.Range(0, CalculateTotalWeight());
    }

    public void DropItem()
    {
        rolledValue = RollTable();
        Debug.Log("Rolled Value: " + rolledValue);
        for(int i=0;i<Weights.Count;i++)
        {
            if(Enabled[i])
            {
                if(rolledValue < Weights[i])
                {
                    Debug.Log("Dropping: " + rolledValue);
                }
                else
                    rolledValue -= Weights[i];
            }
        }
    }
}
