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

            panelProjectileSpeedText.text = (ProjectileSpeed * 60).ToString() + " px/s";
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

    public void ApplyBasicItemEffect(BasicItemType Type)
    {
        switch (Type)
        {
            case BasicItemType.Heal:
                int healAmount = Mathf.RoundToInt(MaxHealth * 0.7f);
                Health += healAmount;
                Debug.Log("BasicItem Heal: +" + healAmount);
                break;

            case BasicItemType.MaxHealthIncrease:
                if (MaxHealth < 2 * initialMaxHealth)
                {
                    MaxHealth += 5;
                    Health += 5;
                }
                Debug.Log("BasicItem MaxHealthIncrease: -> " + MaxHealth);
                break;

            case BasicItemType.MoveSpeedIncrease:
                if (MoveSpeed < initialMoveSpeed + 20 * 5)
                {
                    MoveSpeed += 5;
                }
                Debug.Log("BasicItem MoveSpeedIncrease: -> " + MoveSpeed);
                break;

            case BasicItemType.ProjectileDamageIncrease:
                ProjectileDamage += 15;
                Debug.Log("BasicItem ProjectileDamageIncrease: -> " + ProjectileDamage);
                break;

            case BasicItemType.ShootIntervalDecrease:
                if (ShootInterval > initialShootInterval - 6 * 0.05f)
                {
                    ShootInterval -= 0.05f;
                }
                Debug.Log("BasicItem ShootIntervalDecrease: -> " + ShootInterval);
                break;

            case BasicItemType.ProjectileSpeedIncrease:
                // Initial: 9f (540 px/s), Add: 0.5f (30 px/s), Max: 18f (1080 px/s)
                if (ProjectileSpeed < 18f)
                {
                    ProjectileSpeed += 0.5f;
                }
                Debug.Log("BasicItem ProjectileSpeedIncrease: -> " + ProjectileSpeed * 60);
                break;
            
            case BasicItemType.JumpDurationIncrease:
                if (JumpDuration < 2f)
                {
                    JumpDuration += 0.1f;
                }
                Debug.Log("BasicItem JumpDurationIncrease: -> " + JumpDuration);
                break;

            case BasicItemType.JumpCooldownDecrease:
                if (JumpCooldown > 15f)
                {
                    JumpCooldown -= 0.25f;
                }
                Debug.Log("BasicItem JumpCooldownDecrease: -> " + JumpCooldown);
                break;
        }
    }
}
