using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseEntityController
{
    public GameObject projectilePrefab;
    public GameObject playerPrefab;

    public Text HpText;
    public Text jumpingText;
    public Text jumpText;
    public Text RpowerText;
    public Text RspeedText;
    public Text SspeedText;
    public Text MspeedText;
    public Text Buff1Text;
    public Text Buff2Text;

    public float jumpMaintain;
    public float jumpCooldown;
    public float projectileLifetime;
    public float shootCooldown;
    public int maxHealth;
    public int Damage;
    public GameObject projectile;

    public float transformElapsed;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;

    private SpriteRenderer spriteRenderer;

    private float jumpElapsed;
    private bool isJumping;

    private float buff1Elapsed;
    private float buff2Elapsed;
    private float buff1Maintain;
    private float buff2Maintain;
    private bool buff1working;
    private bool buff2working;
    public bool IsJumping {
        get {
            return isJumping;
        }
        set {
            isJumping = value;
            float jumpRest = jumpCooldown - jumpElapsed;
            if(jumpRest > 0)
            {
                jumpingText.text = isJumping ? "Jumping" : string.Format("Cooldown: {0:f1}", jumpRest);
                jumpText.text = isJumping ? string.Format("Jumping : {0:f1}", jumpMaintain - jumpElapsed) : string.Format("Cooldown: {0:f1}", jumpRest);
            }
            else
            {
                jumpingText.text = "Ready";
                jumpText.text = "Ready";
            }
        }
    }
    public bool Buff1Working
    {
        get
        {
            return buff1working;
        }
        set
        {
            buff1working = value;
            float buff1Rest = buff1Maintain - buff1Elapsed;
            if (buff1Rest > 0)
            {
                Buff1Text.text = buff1working ? string.Format("Remain: {0:f1}"+" s", buff1Rest) : "None";
            }
            else
            {
                Buff1Text.text = "None";
            }
        }
    }
    public bool Buff2Working {
        get {
            return buff2working;
        }
        set {
            buff2working = value;
            float buff2Rest = buff2Maintain - buff2Elapsed;
            if (buff2Rest > 0)
            {
                Buff2Text.text = buff2working ? string.Format("Remain: {0:f1}"+" s", buff2Rest) : "None";
            }
            else
            {
                Buff2Text.text = "None";
            }
        }
    }
    private float shootElapsed;

    new void Start()
    {
        base.Start();

        maxHealth = Health;
        jumpElapsed = jumpCooldown;
        IsJumping = false;

        shootElapsed = shootCooldown;
        transformElapsed = 0.1f;

        spriteRenderer = playerPrefab.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite1;

        Buff1Text.text = "None";
        Buff2Text.text = "None";
        SettingUI();
    }

    void Update()
    {
        MoveAndRotate();
        Jump();
        Shoot();
    }

    private void MoveAndRotate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical);

        if(!(horizontal == 0 && vertical == 0))
        {
            transformElapsed -= Time.deltaTime;
        }
        if (transformElapsed <= 0)
        {
            transformElapsed = 0.1f;
            Transform();
        }

        rb2d.velocity = (speed/60f) * direction.normalized;

        float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        float angle = Mathf.Atan2(mousePosition.y - rb2d.position.y, mousePosition.x - rb2d.position.x) * Mathf.Rad2Deg + 90;

        rb2d.rotation = angle;
    }

    private void Transform()
    {
        if (spriteRenderer.sprite == Sprite1)
        {
            spriteRenderer.sprite = Sprite2;
        }
        else if (spriteRenderer.sprite == Sprite2)
        {
            spriteRenderer.sprite = Sprite3;
        }
        else if (spriteRenderer.sprite == Sprite3)
        {
            spriteRenderer.sprite = Sprite4;
        }
        else
        {
            spriteRenderer.sprite = Sprite1;
        }
    }

    private void Jump()
    {
        jumpElapsed += Time.deltaTime;

        if (Input.GetButtonDown("Jump")) //Space bar > Jump
        {
            if (jumpElapsed >= jumpCooldown) //Initial : 무적 1초 / 종료 후 쿨타임 시작 / 쿨 20초
            {
                jumpElapsed = 0;
            }
        }

        if (jumpElapsed <= jumpMaintain)
        {
            IsJumping = true;
        }
        else
        {
            IsJumping = false;
        }
    }

    private void Shoot()
    {
        shootElapsed += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (shootElapsed >= shootCooldown)
            {
                shootElapsed = 0;

                projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
                Destroy(projectile, projectileLifetime);

                float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
                Vector2 mouseDirection = mousePosition - rb2d.position;

                projectile.GetComponent<PlayerProjectileController>().SetDirection(mouseDirection);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (!IsJumping)
            {
                Health -= 20;
            }
            else
            {
                Debug.Log("회피 성공.");
            }
            CheckGameOver();
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy Projectile")
        {
            if (!IsJumping)
            {
                Health -= 25;
            }
            else
            {
                Debug.Log("회피 성공.");
            }
            CheckGameOver();
        }
        else if (col.gameObject.tag == "Item")
        {
            ItemController GotItem = col.gameObject.GetComponent<ItemController>();
            switch(GotItem.BSType)
            {
                case "Basic":
                    ItemType Type = GotItem.type;
                    ItemEffect(Type);
                    break;
                case "Buff":
                    BuffType Typebf = GotItem.BfType;
                    BuffEffect(Typebf);
                    break;
                //case "Boss":
                //    BossType Typebo = GotItem.BoType;
                //    BossEffect(Typebo);
                //    break;
            }
        }
        else if (col.gameObject.tag == "Redzone")
        {
            Health = 0; //Instant death
        }
        SettingUI();
    }

    private void ItemEffect(ItemType Type)
    {
        switch (Type)
        {
            case ItemType.Heal:
                int healAmount = (int) (maxHealth * 0.7);
                if (maxHealth < Health + healAmount)
                {
                    healAmount = maxHealth - Health;
                }
                Health += healAmount;
                Debug.Log("Healed " + healAmount.ToString() + " HP.");
                break;
            case ItemType.Rspeed:
                //Rspeed : Initial : 9 (540 px) Add : 0.5f (30 px) Max 18 (1080 px)
                Debug.Log("Rspeed is now " + "540" + " px/s.");
                break;
            case ItemType.Sspeed:
                if(shootCooldown > 0.2f)
                {
                    shootCooldown -= 0.05f;
                }
                Debug.Log("Sspeed is now " + shootCooldown + " s.");
                break;
            case ItemType.Rpower:
                Damage += 15;
                Debug.Log("Rpower is now " + Damage.ToString() + ".");
                break;
            case ItemType.Mspeed:
                if (speed < 400)
                {
                    speed += 5;
                }
                Debug.Log("Mspeed is now " + speed.ToString() + " px/s.");
                break;
            case ItemType.JumpM:
                if(jumpMaintain < 2)
                {
                    jumpMaintain += 0.1f;
                }
                Debug.Log("JumpM is now " + jumpMaintain.ToString() + " s.");
                break;
            case ItemType.JumpCD:
                if(jumpCooldown>15)
                {
                    jumpCooldown -= 0.25f;
                }
                Debug.Log("JumpCD is now " + jumpCooldown.ToString() + " s.");
                break;
            case ItemType.MaxHpUp:
                if(maxHealth < 200)
                {
                    maxHealth += 5;
                }
                Health += 5;
                Debug.Log("MaxHP is now " + maxHealth.ToString() + ".");
                break;
        }
    }

    private void BuffEffect(BuffType Type)
    {
        switch (Type)
        {
            case BuffType.JumpBf:
                Debug.Log("Use Jump Buff");
                break;
            case BuffType.RspeedBf:
                Debug.Log("Use Rspeed Buff");
                break;
            case BuffType.SspeedBf:
                Debug.Log("Use Sspeed Buff");
                break;
            case BuffType.RpowerBf:
                Debug.Log("Use Rpower Buff");
                break;
            case BuffType.MspeedBf:
                Debug.Log("Use Mspeed Buff");
                break;
            case BuffType.AspeedBf:
                Debug.Log("Use All speed Buff");
                break;
        }
    }

    //private void BossEffect(BossType Type)
    //{
    //    Debug.Log("Hello?");
    //}

    private void SettingUI()
    {
        HpText.text = Health.ToString() + " / " + maxHealth.ToString();
        RpowerText.text = Damage.ToString();
        RspeedText.text = "540 px/s";
        SspeedText.text = shootCooldown.ToString() + " s";
        MspeedText.text = speed.ToString() + " px/s";
    }

    private void CheckGameOver()
    {
        if (Health <= 0)
        {
            //Debug.LogError("Game Over");
        }
    }
}
