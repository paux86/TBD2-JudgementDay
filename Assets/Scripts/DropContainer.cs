using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropContainer : MonoBehaviour
{
    [SerializeField] UsableItem item;
    [SerializeField] Weapon weapon;

    int type;
    const int WEAPON_TYPE = 0;
    const int ITEM_TYPE = 1;

    public void UpdateType(int type)
    {
        switch(type)
        {
            case WEAPON_TYPE:
            case ITEM_TYPE:
                this.type = type;
                break;
            default:
                Debug.Log("Wrong type of type in dropcontainer");
                break;
        }
    }

    public int GetItemType()
    {
        return this.type;
    }

    public void SetWeapon(Weapon weapon)
    {

        this.weapon = weapon;
    }

    public void SetItem(UsableItem item)
    {
        this.item = item;
    }

    public Weapon GetWeapon()
    {
        Weapon weapon; 
        if(type == WEAPON_TYPE)
        {
            weapon = this.weapon;
        }
        else
        {
            weapon = null;

            Debug.Log("This dropcontainer doesn't contain a weapon");
        }

        return weapon;
    }

    public UsableItem GetItem()
    {
        UsableItem item;

        if(type == ITEM_TYPE)
        {
            item = this.item;
        }
        else
        {
            item = null;
            Debug.Log("This dropcontainer doesn't contain an item");
        }

        return item;
    }

}
