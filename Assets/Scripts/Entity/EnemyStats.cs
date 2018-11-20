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

    protected override void Start()
    {
        base.Start();

        MoveSpeed = initialMoveSpeed;
        MoveInterval = initialMoveInterval;
        ShootInterval = initialShootInterval;
        ProjectileDamage = initialProjectileDamage;
        ProjectileSpeed = initialProjectileSpeed;
        ProjectileLifetime = initialProjectileLifetime;

        WaveReinforce();
    }

    protected virtual void WaveReinforce()
    {
        int wave = GameObject.FindWithTag("GameController").GetComponent<GameManager>().Wave;

        switch (wave)
        {
            case 2:
                MaxHealth = 125;
                Health = 125;
                break;

            case 3:
                MaxHealth = 150;
                Health = 150;
                ProjectileDamage = 33;
                break;

            case 4:
                MaxHealth = 175;
                Health = 175;
                ProjectileDamage = 33;
                break;

            case 5:
                MaxHealth = 200;
                Health = 200;
                ProjectileDamage = 40;
                break;
        }
    }
}
