using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { House, German, American, Lab1, Lab2 }

public static class InitialPlayerStats
{
    private static PlayerType? _type;

    public static int MaxHealth { get; set; }
    public static float MoveSpeed { get; set; }
    public static float ShootInterval { get; set; }
    public static int ProjectileDamage { get; set; }
    public static float ProjectileSpeed { get; set; }
    public static float ProjectileLifetime { get; set; }
    public static float JumpDuration { get; set; }
    public static float JumpCooldown { get; set; }
    public static PlayerType? Type {
        get {
            return _type;
        }
        set {
            _type = value;

            MaxHealth = 100;
            MoveSpeed = 200;
            ShootInterval = 0.5f;
            ProjectileDamage = 60;
            ProjectileSpeed = 9;
            ProjectileLifetime = 2;
            JumpDuration = 1;
            JumpCooldown = 20;

            switch (Type)
            {
                case PlayerType.House:
                    break;

                case PlayerType.German:
                    ShootInterval -= ShootInterval * 0.2f;
                    MaxHealth -= Mathf.RoundToInt(MaxHealth * 0.2f);
                    break;

                case PlayerType.American:
                    MaxHealth += Mathf.RoundToInt(MaxHealth * 0.25f);
                    break;

                case PlayerType.Lab1:
                    ProjectileDamage /= 3;
                    // shoot 3 lines
                    break;

                case PlayerType.Lab2:
                    MaxHealth /= 2;
                    JumpCooldown *= 2 / 3.0f;
                    break;
            }
        }
    }
}
