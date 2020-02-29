using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int attackDamage;
    public float attackCooldownTime;
    public int range;
    public Sprite onHitEffect;
    public bool hitscan;
    public GameObject projectile;
}
