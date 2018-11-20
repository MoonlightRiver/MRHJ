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
    }

    public void WaveReinforce(int wave) // Not working well.
    {
        if(wave == 2)
        {
            //health..어딨어요?
        }
        else if(wave == 3)
        {
            ProjectileDamage = 33;
        }
        else if(wave == 4)
        {
            //health..
        }
        else // wave >= 5
        {
            ProjectileDamage = 40;
        }
        //Debug.Log("Reinforced");
    }
}
