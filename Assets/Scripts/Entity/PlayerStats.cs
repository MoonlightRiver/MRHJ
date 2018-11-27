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
    public Text[] panelBuffText;
    public Image[] panelBuffImage;
    public Text[] subpanelBuffText;
    public Image[] subpanelBuffImage;
    public Sprite knob;

    private float baseMoveSpeed;
    private float baseShootInterval;
    private int baseProjectileDamage;
    private float baseProjectileSpeed;
    private float baseProjectileLifetime;
    private float baseJumpDuration;
    private float baseJumpCooldown;
    private float buffMoveSpeed;
    private float buffShootInterval;
    private int buffProjectileDamage;
    private float buffProjectileSpeed;
    private float buffProjectileLifetime;
    private float buffJumpDuration;
    private float buffJumpCooldown;

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
            return baseMoveSpeed + buffMoveSpeed;
        }
    }
    public float ShootInterval {
        get {
            return baseShootInterval + buffShootInterval;
        }
    }
    public int ProjectileDamage {
        get {
            return baseProjectileDamage + buffProjectileDamage;
        }
    }
    public float ProjectileSpeed {
        get {
            return baseProjectileSpeed + buffProjectileSpeed;
        }
    }
    public float ProjectileLifetime {
        get {
            return baseProjectileLifetime + buffProjectileLifetime;
        }
    }
    public float JumpDuration {
        get {
            return baseJumpDuration + buffJumpDuration;
        }
    }
    public float JumpCooldown {
        get {
            return baseJumpCooldown + buffJumpCooldown;
        }
    }
    public int ShootLineNum { get; private set; }

    private List<BuffItemController> buffs;
    private List<int> buffRemainingTimes;
    private List<Coroutine> buffRemoveCoroutines;
    private GameManager gameManager;
    private int originalBaseProjectileDamage;

    protected override void Start()
    {
        if (InitialPlayerStats.Type == null)
        {
            InitialPlayerStats.Type = PlayerType.House;
        }
        Debug.Log("Player Type: " + InitialPlayerStats.Type);

        MaxHealth = InitialPlayerStats.MaxHealth;
        Health = InitialPlayerStats.MaxHealth;

        baseMoveSpeed = InitialPlayerStats.MoveSpeed;
        baseShootInterval = InitialPlayerStats.ShootInterval;
        baseProjectileDamage = InitialPlayerStats.ProjectileDamage;
        baseProjectileSpeed = InitialPlayerStats.ProjectileSpeed;
        baseProjectileLifetime = InitialPlayerStats.ProjectileLifetime;
        baseJumpDuration = InitialPlayerStats.JumpDuration;
        baseJumpCooldown = InitialPlayerStats.JumpCooldown;

        buffMoveSpeed = 0;
        buffShootInterval = 0;
        buffProjectileDamage = 0;
        buffProjectileSpeed = 0;
        buffProjectileLifetime = 0;
        buffJumpDuration = 0;
        buffJumpCooldown = 0;

        ShootLineNum = InitialPlayerStats.ShootLineNum;

        buffs = new List<BuffItemController>();
        buffRemainingTimes = new List<int>();
        buffRemoveCoroutines = new List<Coroutine>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        originalBaseProjectileDamage = baseProjectileDamage;

        UpdatePanelStats();
        UpdatePanelBuffs();
    }

    void Update()
    {
        if (gameManager.IsDebugMode)
        {
            baseProjectileDamage = 500;
        }
        else
        {
            baseProjectileDamage = originalBaseProjectileDamage;
        }
        UpdatePanelStats();
    }

    public void ApplyBasicItemEffect(BasicItemController basicItem)
    {
        switch (basicItem.Type)
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
                if (baseMoveSpeed < InitialPlayerStats.MoveSpeed + 20 * 5)
                {
                    baseMoveSpeed += 5;
                }
                Debug.Log("BasicItem MoveSpeedIncrease: -> " + baseMoveSpeed);
                break;

            case BasicItemType.ShootIntervalDecrease:
                if (baseShootInterval > InitialPlayerStats.ShootInterval - 6 * 0.05f)
                {
                    baseShootInterval -= 0.05f;
                }
                Debug.Log("BasicItem ShootIntervalDecrease: -> " + baseShootInterval);
                break;

            case BasicItemType.ProjectileDamageIncrease:
                baseProjectileDamage += 15;
                originalBaseProjectileDamage = baseProjectileDamage;
                Debug.Log("BasicItem ProjectileDamageIncrease: -> " + baseProjectileDamage);
                break;

            case BasicItemType.ProjectileSpeedIncrease:
                // Initial: 9f (540 px/s), Add: 0.5f (30 px/s), Max: 18f (1080 px/s)
                if (baseProjectileSpeed < 18f)
                {
                    baseProjectileSpeed += 0.5f;
                }
                Debug.Log("BasicItem ProjectileSpeedIncrease: -> " + baseProjectileSpeed * 60);
                break;
            
            case BasicItemType.JumpDurationIncrease:
                if (baseJumpDuration < 2f)
                {
                    baseJumpDuration += 0.1f;
                }
                Debug.Log("BasicItem JumpDurationIncrease: -> " + baseJumpDuration);
                break;

            case BasicItemType.JumpCooldownDecrease:
                if (baseJumpCooldown > 15f)
                {
                    baseJumpCooldown -= 0.25f;
                }
                Debug.Log("BasicItem JumpCooldownDecrease: -> " + baseJumpCooldown);
                break;
        }

        UpdatePanelStats();
    }

    public void AddBuff(BuffItemController buffItem)
    {
        if (buffs.Count >= 2)
        {
            StopCoroutine(buffRemoveCoroutines[0]);
            buffRemoveCoroutines.RemoveAt(0);
            StartCoroutine(RemoveBuff(buffs[0], 0));
        }

        int removeAfter = 0;
        switch (buffItem.Type)
        {
            case BuffItemType.AllSpeedIncrease:
                buffMoveSpeed += 10;
                buffShootInterval += -0.1f;
                buffProjectileSpeed += 1;
                removeAfter = 30;
                break;

            case BuffItemType.MoveSpeedIncrease:
                buffMoveSpeed += 10;
                removeAfter = 120;
                break;

            case BuffItemType.ShootIntervalDecrease:
                buffShootInterval += -0.1f;
                removeAfter = 120;
                break;

            case BuffItemType.ProjectileDamageIncrease:
                buffProjectileDamage += 30;
                removeAfter = 120;
                break;

            case BuffItemType.ProjectileSpeedIncrease:
                buffProjectileSpeed += 1;
                removeAfter = 120;
                break;

            case BuffItemType.Jump:
                buffJumpDuration += 5;
                buffJumpCooldown += -5;
                removeAfter = 60;
                break;
        }
        buffs.Add(buffItem);
        buffRemainingTimes.Add(removeAfter);
        buffRemoveCoroutines.Add(StartCoroutine(RemoveBuff(buffItem, removeAfter)));

        UpdatePanelBuffs();
    }

    private void UpdatePanelStats()
    {
        panelMoveSpeedText.text = baseMoveSpeed.ToString() + " px/s";
        panelProjectileCooldownText.text = baseShootInterval.ToString() + " s";
        panelProjectileDamageText.text = baseProjectileDamage.ToString();
        panelProjectileSpeedText.text = (baseProjectileSpeed * 60).ToString() + " px/s";
    }

    private IEnumerator RemoveBuff(BuffItemController buffItem, int after)
    {
        int index = -1;

        for (int remaining = after; remaining > 0; remaining--)
        {
            index = buffs.IndexOf(buffItem);
            buffRemainingTimes[index] = remaining;
            UpdatePanelBuffs();

            yield return new WaitForSeconds(1);
        }
        index = buffs.IndexOf(buffItem);
        buffRemainingTimes[index] = 0;
        UpdatePanelBuffs();

        switch (buffItem.Type)
        {
            case BuffItemType.AllSpeedIncrease:
                buffMoveSpeed -= 10;
                buffShootInterval -= -0.1f;
                buffProjectileSpeed -= 1;
                break;

            case BuffItemType.MoveSpeedIncrease:
                buffMoveSpeed -= 10;
                break;

            case BuffItemType.ShootIntervalDecrease:
                buffShootInterval -= -0.1f;
                break;


            case BuffItemType.ProjectileDamageIncrease:
                buffProjectileDamage -= 30;
                break;

            case BuffItemType.ProjectileSpeedIncrease:
                buffProjectileSpeed -= 1;
                break;

            case BuffItemType.Jump:
                buffJumpDuration -= 5;
                buffJumpCooldown -= -5;
                break;
        }

        buffs.RemoveAt(index);
        buffRemainingTimes.RemoveAt(index);

        UpdatePanelBuffs();
    }

    private void UpdatePanelBuffs()
    {
        for (int i = 0; i < panelBuffText.Length; i++)
        {
            if (i < buffs.Count)
            {
                panelBuffText[i].text = buffRemainingTimes[i].ToString();
                panelBuffImage[i].sprite = buffs[i].Sprite;
                subpanelBuffText[i].text = buffRemainingTimes[i].ToString();
                subpanelBuffImage[i].sprite = buffs[i].Sprite;
            }
            else
            {
                panelBuffText[i].text = "";
                panelBuffImage[i].sprite = knob;
                subpanelBuffText[i].text = "";
                subpanelBuffImage[i].sprite = knob;
            }
        }
    }
}
