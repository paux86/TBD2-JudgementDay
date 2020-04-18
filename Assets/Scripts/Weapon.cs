using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;
    public string description;
    public int cost;
    public Sprite sprite;
    public int attackDamage;
    public float attackCooldownTime;
    public float range;
    public Sprite onHitEffect;
    public bool hitscan;
    public bool isMeleeWeapon;
    public GameObject projectile;
    public Sprite buttonSprite;

    //the data here is all public and can be accessed directly
    /*
    public int GetAttackDamage()
    {
        return this.attackDamage;
    }*/
}
