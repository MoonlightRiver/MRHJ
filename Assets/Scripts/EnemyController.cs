using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Heal, Rspeed, Sspeed, Rpower, Mspeed, JumpM, JumpCD, MaxHpUp };

public class EnemyController : BaseEntityController
{
    public GameObject projectilePrefab;
    public GameObject ItemPrefab;

    public float getItemRate;
    public float moveInterval;
    public float shootPlayerInterval;
    public float projectileLifetime;

    private GameManager gameManager;
    private Rigidbody2D playerRb2d;

    public Sprite CurrentSprite;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;
    public Sprite Sprite6;
    public Sprite Sprite7;
    public Sprite Sprite8;
    private SpriteRenderer spriteRenderer;

    public float HealRate;
    public float RspeedRate;
    public float SspeedRate;
    public float RpowerRate;
    public float MspeedRate;
    public float JumpMRate;
    public float JumpCDRate;
    public float MaxHpUpRate;

    public ItemType Type { get; private set; }

    new void Start()
    {
        base.Start();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        spriteRenderer = ItemPrefab.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CurrentSprite;

        StartCoroutine(Move());
        StartCoroutine(ShootPlayer());
    }

    void Update()
    {
        if (Health <= 0)
        {
            gameManager.Score += 100;
            GiveItem();
            Destroy(gameObject);
        }

        if ((rb2d.position - playerRb2d.position).magnitude >= despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player Projectile")
        {
            PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            Health -= player.Damage;
        }
    }

    void GiveItem()
    {
        float itemslot = Random.Range(0f, 1f);
        if (itemslot <= getItemRate/15f)
        {
            Type = SlotMachine();
            ItemController NewItem = ItemPrefab.GetComponent<ItemController>();
            NewItem.type = Type;
            MatchImage(Type, spriteRenderer);

            Instantiate(ItemPrefab, rb2d.position, Quaternion.identity);
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            float horizontal = Random.Range(-1f, 1f);
            float vertical = Random.Range(-1f, 1f);

            Vector2 direction = new Vector2(horizontal, vertical).normalized;

            rb2d.velocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rb2d.rotation = angle;

            yield return new WaitForSeconds(moveInterval);
        }
    }

    private IEnumerator ShootPlayer()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
            Destroy(projectile, projectileLifetime);

            Vector2 playerDirection = playerRb2d.position - rb2d.position;
            projectile.GetComponent<EnemyProjectileController>().SetDirection(playerDirection);

            yield return new WaitForSeconds(shootPlayerInterval);
        }
    }

    ItemType SlotMachine()
    {
        float ItemSlot = Random.Range(0f, 100f);
        if (ItemSlot <= HealRate)
        {
            return ItemType.Heal;
        }
        else if (ItemSlot <= HealRate + RspeedRate)
        {
            return ItemType.Rspeed;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate)
        {
            return ItemType.Sspeed;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate)
        {
            return ItemType.Rpower;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate + MspeedRate)
        {
            return ItemType.Mspeed;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate + MspeedRate + JumpMRate)
        {
            return ItemType.JumpM;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate + MspeedRate + JumpMRate + JumpCDRate)
        {
            return ItemType.JumpCD;
        }
        else
        {
            return ItemType.MaxHpUp;
        }
    }

    void MatchImage(ItemType Type, SpriteRenderer spriteRenderer)
    {
        switch (Type)
        {
            case ItemType.Heal:
                spriteRenderer.sprite = Sprite1;
                break;
            case ItemType.Rspeed:
                spriteRenderer.sprite = Sprite2;
                break;
            case ItemType.Sspeed:
                spriteRenderer.sprite = Sprite3;
                break;
            case ItemType.Rpower:
                spriteRenderer.sprite = Sprite4;
                break;
            case ItemType.Mspeed:
                spriteRenderer.sprite = Sprite5;
                break;
            case ItemType.JumpM:
                spriteRenderer.sprite = Sprite6;
                break;
            case ItemType.JumpCD:
                spriteRenderer.sprite = Sprite7;
                break;
            case ItemType.MaxHpUp:
                spriteRenderer.sprite = Sprite8;
                break;
        }
    }
}