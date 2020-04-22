using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    [SerializeField] AttackWithWeapon attack = null;
    [SerializeField] PlayerStats player = null;

    private void Start()
    {
        Swap(0);
    }

    public void Swap()
    {
        player.SwapToNextWeapon();
        UpdateAttackWeapon();
    }

    public void Swap(int weaponSlot)
    {
        player.ChangeCurrentWeapon(weaponSlot);
        UpdateAttackWeapon();
    }

    private void UpdateAttackWeapon()
    {
        attack.equippedWeapon = player.currentWeapon;
    }
}
