using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodStats : BaseEntityStats
{
    public Text hitCountText;

    public int initialProjectileDamage;
    public float initialProjectileSpeed;
    public float initialProjectileLifetime;

    private int _hitCount;

    public int HitCount {
        get {
            return _hitCount;
        }
        set {
            _hitCount = value;

            hitCountText.text = "Hit: " + HitCount.ToString();
        }
    }
    public int ProjectileDamage { get; protected set; }
    public float ProjectileSpeed { get; protected set; }
    public float ProjectileLifetime { get; protected set; }

    protected override void Start()
    {
        HitCount = 0;

        ProjectileDamage = initialProjectileDamage;
        ProjectileSpeed = initialProjectileSpeed;
        ProjectileLifetime = initialProjectileLifetime;
    }
}
