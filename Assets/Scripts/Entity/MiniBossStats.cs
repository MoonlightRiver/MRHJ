using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossStats : EnemyStats
{
    // Can be edited to produce behaviors different from enemy
    protected override void WaveReinforce()
    {
        int wave = 5;

        switch (wave)
        {
            case 5:
                MaxHealth = 2000;
                Health = 2000;
                ProjectileDamage = 50;
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
                break;
        }
    }
}
