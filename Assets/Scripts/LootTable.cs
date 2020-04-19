using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Loot Table", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public List<ScriptableObject> Drops;
    public List<int> Weights;
    public List<bool> Enabled;
    ItemSpawner itemSpawner;
    private int totalWeight = 0;
    private int rolledValue = -1;

    public void DropItem()
    {
        rolledValue = UnityEngine.Random.Range(0, totalWeight);
        Debug.Log("Rolled Random Value: " + rolledValue);
        for(int i=0;i<Weights.Count;i++)
        {
            Debug.Log("Loop #: " + (i + 1) + " First Check - Comparing " + rolledValue + " to " + Weights[i]);
            if(rolledValue <= Weights[i])
            {
                Debug.Log("Loop #: " + (i + 1) + " Second Check - Is Element " + i + " enabled?");
                if(Enabled[i] && rolledValue > -1)
                {
                    //////////////////////////////////////////////////////////
                    Debug.Log("Loop #: " + (i + 1) + " Dropping: " + i);

                    

                    //////////////////////////////////////////////////////////
                }
                else
                    Debug.Log("Loop #: " + (i + 1) + " Element " + i + " is not enabled or " + rolledValue + " <= -1");
                rolledValue = -1;
                Debug.Log("Loop #: " + (i + 1) + " Setting rolled value to -1");
            }
            else if(rolledValue != -1)
            {
                Debug.Log("Loop #: " + (i + 1) + " Else - Rolled Value: " + rolledValue);
                Debug.Log("Loop #: " + (i + 1) + " Else - Subtracting " + rolledValue + " by " + Weights[i]);
                rolledValue -= Weights[i];
                Debug.Log("Loop #: " + (i + 1) + " Else - New Rolled Value: " + rolledValue);
            }
        }
    }
}
