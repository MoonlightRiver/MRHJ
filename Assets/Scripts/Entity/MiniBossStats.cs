using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossStats : EnemyStats
{
    protected override void WaveReinforce()
    {
        // Can be edited to produce behaviors different from enemy
        int wave = GameObject.FindWithTag("GameController").GetComponent<GameManager>().Wave;

        switch (wave)
        {
            case 2:
                MaxHealth = 1025;
                Health = 1025;
                break;

            case 3:
                MaxHealth = 1050;
                Health = 1050;
                ProjectileDamage = 33;
                break;

            case 4:
                MaxHealth = 1075;
                Health = 1075;
                ProjectileDamage = 33;
                break;

            case 5:
                MaxHealth = 2000;
                Health = 2000;
                ProjectileDamage = 40;
                break;
        }
    }
}
