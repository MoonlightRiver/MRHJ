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

    public float MoveSpeed { get; protected set; }
    public float MoveInterval { get; protected set; }
    public float ShootInterval { get; protected set; }
    public int ProjectileDamage { get; protected set; }
    public float ProjectileSpeed { get; protected set; }
    public float ProjectileLifetime { get; protected set; }
    public int ShootBurstNum { get; protected set; }
    public int ShootLineNum { get; protected set; }

    protected override void Start()
    {
        base.Start();

        MoveSpeed = initialMoveSpeed;
        MoveInterval = initialMoveInterval;
        ShootInterval = initialShootInterval;
        ProjectileDamage = initialProjectileDamage;
        ProjectileSpeed = initialProjectileSpeed;
        ProjectileLifetime = initialProjectileLifetime;
        ShootBurstNum = 1;
        ShootLineNum = 1;

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
                ShootBurstNum = 3;
                break;

            case 3:
                MaxHealth = 150;
                Health = 150;
                ProjectileDamage = 33;
                ShootBurstNum = 3;
                ShootLineNum = 3;
                break;

            case 4:
                MaxHealth = 175;
                Health = 175;
                ProjectileDamage = 33;
                if (Random.Range(1, 3) == 1)
                {
                    ShootBurstNum = 3;
                    ShootLineNum = 3;
                }
                else
                {
                    ShootBurstNum = 7;
                }
                break;

            case 5:
                MaxHealth = 200;
                Health = 200;
                ProjectileDamage = 40;
                if (Random.Range(1, 3) == 1)
                {
                    ShootBurstNum = 3;
                    ShootLineNum = 5;
                }
                else
                {
                    ShootBurstNum = 9;
                }
                break;
        }
    }
}
