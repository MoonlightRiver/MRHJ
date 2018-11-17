using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType { JumpBf, RspeedBf, SspeedBf, RpowerBf, MspeedBf, AspeedBf };

public class MiniBossController : BaseEntityController
{
    public GameObject projectilePrefab;
    public GameObject ItemPrefab;

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
    private SpriteRenderer spriteRenderer;
    
    public BuffType TypeBf { get; private set; }

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
            gameManager.Score += 500;
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
        TypeBf = SlotMachine();
        ItemController NewItem = ItemPrefab.GetComponent<ItemController>();
        NewItem.BfType = TypeBf;
        NewItem.BSType = "Buff";
        MatchImage(TypeBf, spriteRenderer);

        Instantiate(ItemPrefab, rb2d.position, Quaternion.identity);
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

    BuffType SlotMachine()
    {
        float ItemSlot = Random.Range(0f, 6f);
        if (ItemSlot <= 1f)
        {
            return BuffType.JumpBf;
        }
        else if (ItemSlot <= 2f)
        {
            return BuffType.RspeedBf;
        }
        else if (ItemSlot <= 3f)
        {
            return BuffType.SspeedBf;
        }
        else if (ItemSlot <= 4f)
        {
            return BuffType.RpowerBf;
        }
        else if (ItemSlot <= 5f)
        {
            return BuffType.MspeedBf;
        }
        else
        {
            return BuffType.AspeedBf;
        }
    }

    void MatchImage(BuffType TypeBf, SpriteRenderer spriteRenderer)
    {
        switch (TypeBf)
        {
            case BuffType.JumpBf:
                spriteRenderer.sprite = Sprite1;
                break;
            case BuffType.RspeedBf:
                spriteRenderer.sprite = Sprite2;
                break;
            case BuffType.SspeedBf:
                spriteRenderer.sprite = Sprite3;
                break;
            case BuffType.RpowerBf:
                spriteRenderer.sprite = Sprite4;
                break;
            case BuffType.MspeedBf:
                spriteRenderer.sprite = Sprite5;
                break;
            case BuffType.AspeedBf:
                spriteRenderer.sprite = Sprite6;
                break;
        }
    }
}