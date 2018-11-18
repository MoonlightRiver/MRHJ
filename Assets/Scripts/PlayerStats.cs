using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : BaseEntityStats
{
    public Text panelHealthText;
    public Text panelMovementSpeedText;
    public Text panelProjectileDamageText;
    public Text panelProjectileCooldownText;
    public Text panelProjectileSpeedText;

    public int initialProjectileDamage;
    public float initialProjectileCooldown;
    public float initialProjectileSpeed;
    public float initialProjectileLifetime;
    public float initialJumpDuration;
    public float initialJumpCooldown;

    public override int Health {
        get {
            return base.Health;
        }
        set {
            base.Health = value;
            panelHealthText.text = string.Format("HP: {0}/{1}", Health, MaxHealth);
        }
    }
    public override float MovementSpeed {
        get {
            return base.MovementSpeed;
        }
        set {
            base.MovementSpeed = value;
            panelMovementSpeedText.text = MovementSpeed.ToString() + " px/s";
        }
    }
    private int _projectileDamage;
    public int ProjectileDamage {
        get {
            return _projectileDamage;
        }
        set {
            _projectileDamage = value;
            panelProjectileDamageText.text = _projectileDamage.ToString();
        }
    }
    private float _projectileCooldown;
    public float ProjectileCooldown {
        get {
            return _projectileCooldown;
        }
        set {
            _projectileCooldown = value;
            panelProjectileCooldownText.text = _projectileCooldown.ToString() + " s";
        }
    }
    private float _projectileSpeed;
    public float ProjectileSpeed {
        get {
            return _projectileSpeed;
        }
        set {
            _projectileSpeed = value;
            panelProjectileSpeedText.text = _projectileSpeed.ToString() + " px/s";
        }
    }
    public float ProjectileLifetime { get; set; }
    public float JumpDuration { get; set; }
    public float JumpCooldown { get; set; }

    protected override void Start()
    {
        base.Start();

        ProjectileDamage = initialProjectileDamage;
        ProjectileCooldown = initialProjectileCooldown;
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

            case ItemType.MovementSpeedIncrease:
                if (MovementSpeed < 400)
                {
                    MovementSpeed += 5;
                }
                Debug.Log("Mspeed is now " + MovementSpeed.ToString() + " px/s.");
                break;

            case ItemType.ProjectileDamageIncrease:
                ProjectileDamage += 15;
                Debug.Log("Rpower is now " + ProjectileDamage.ToString() + ".");
                break;

            case ItemType.ProjectileCooldownDecrease:
                if (ProjectileCooldown > 0.2f)
                {
                    ProjectileCooldown -= 0.05f;
                }
                Debug.Log("Sspeed is now " + ProjectileCooldown + " s.");
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
