using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : BaseEntityStats
{
    public float initialMoveSpeed;
    public float initialMoveInterval;
    public float initialShootInterval;
    public int initialProjectileDamage;
    public float initialProjectileSpeed;
    public float initialProjectileLifetime;

    public float MoveSpeed { get; set; }
    public float MoveInterval { get; set; }
    public float ShootInterval { get; set; }
    public int ProjectileDamage { get; set; }
    public float ProjectileSpeed { get; set; }
    public float ProjectileLifetime { get; set; }
    public float ItemDropRate { get; set; }

    protected override void Start()
    {
        base.Start();

        MoveSpeed = initialMoveSpeed;
        MoveInterval = initialMoveInterval;
        ShootInterval = initialShootInterval;
        ProjectileDamage = initialProjectileDamage;
        ProjectileSpeed = initialProjectileSpeed;
        ProjectileLifetime = initialProjectileLifetime;
    }
}
