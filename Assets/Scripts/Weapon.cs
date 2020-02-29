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
    public int attackRate;
    public int range;
    public Sprite onHitEffect;
    public bool hitscan;
    public float projectileSpeed;
    public Sprite projectileSprite;
    public Collider2D hurtBox;
}
