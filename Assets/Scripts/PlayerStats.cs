using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : BaseEntityStats
{
    public Text panelHealthText;
    public Text panelMoveSpeedText;
    public Text panelProjectileDamageText;
    public Text panelProjectileCooldownText;
    public Text panelProjectileSpeedText;

    public float initialMoveSpeed;
    public float initialShootInterval;
    public int initialProjectileDamage;
    public float initialProjectileSpeed;
    public float initialProjectileLifetime;
    public float initialJumpDuration;
    public float initialJumpCooldown;

    private float _moveSpeed;
    private float _shootInterval;
    private int _projectileDamage;
    private float _projectileSpeed;

    public override int Health {
        get {
            return base.Health;
        }
        set {
            base.Health = value;

            panelHealthText.text = string.Format("HP: {0}/{1}", Health, MaxHealth);
        }
    }
    public float MoveSpeed {
        get {
            return _moveSpeed;
        }
        set {
            _moveSpeed = value;

            panelMoveSpeedText.text = MoveSpeed.ToString() + " px/s";
        }
    }
    public float ShootInterval {
        get {
            return _shootInterval;
        }
        set {
            _shootInterval = value;

            panelProjectileCooldownText.text = ShootInterval.ToString() + " s";
        }
    }
    public int ProjectileDamage {
        get {
            return _projectileDamage;
        }
        set {
            _projectileDamage = value;

            panelProjectileDamageText.text = ProjectileDamage.ToString();
        }
    }
    public float ProjectileSpeed {
        get {
            return _projectileSpeed;
        }
        set {
            _projectileSpeed = value;

            panelProjectileSpeedText.text = ProjectileSpeed.ToString() + " px/s";
        }
    }
    public float ProjectileLifetime { get; set; }
    public float JumpDuration { get; set; }
    public float JumpCooldown { get; set; }

    protected override void Start()
    {
        base.Start();

        MoveSpeed = initialMoveSpeed;
        ShootInterval = initialShootInterval;
        ProjectileDamage = initialProjectileDamage;
        ProjectileSpeed = initialProjectileSpeed;
        ProjectileLifetime = initialProjectileLifetime;
        JumpDuration = initialJumpDuration;
        JumpCooldown = initialJumpCooldown;
    }

    public void ItemEffect(ItemType Type)
    {
        switch (Type)
        {
            case ItemType.Heal:
                int healAmount = (int)(MaxHealth * 0.7);
                if (MaxHealth < Health + healAmount)
                {
                    healAmount = MaxHealth - Health;
                }
                Health += healAmount;
                Debug.Log("Healed " + healAmount.ToString() + " HP.");
                break;

            case ItemType.MaxHealthIncrease:
                if (MaxHealth < 200)
                {
                    MaxHealth += 5;
                }
                Health += 5;
                Debug.Log("MaxHP is now " + MaxHealth.ToString() + ".");
                break;

            case ItemType.MoveSpeedIncrease:
                if (MoveSpeed < 400)
                {
                    MoveSpeed += 5;
                }
                Debug.Log("Mspeed is now " + MoveSpeed.ToString() + " px/s.");
                break;

            case ItemType.ProjectileDamageIncrease:
                ProjectileDamage += 15;
                Debug.Log("Rpower is now " + ProjectileDamage.ToString() + ".");
                break;

            case ItemType.ShootIntervalDecrease:
                if (ShootInterval > 0.2f)
                {
                    ShootInterval -= 0.05f;
                }
                Debug.Log("Sspeed is now " + ShootInterval + " s.");
                break;

            case ItemType.ProjectileSpeedIncrease:
                //Rspeed : Initial : 9 (540 px) Add : 0.5f (30 px) Max 18 (1080 px)
                Debug.Log("Rspeed is now " + "540" + " px/s.");
                break;
            
            case ItemType.JumpDurationIncrease:
                if (JumpDuration < 2)
                {
                    JumpDuration += 0.1f;
                }
                Debug.Log("JumpM is now " + JumpDuration.ToString() + " s.");
                break;

            case ItemType.JumpCooldownDecrease:
                if (JumpCooldown > 15)
                {
                    JumpCooldown -= 0.25f;
                }
                Debug.Log("JumpCD is now " + JumpCooldown.ToString() + " s.");
                break;
        }
    }
}
