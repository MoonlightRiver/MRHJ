using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseEntityController
{
    public GameObject projectilePrefab;
    public GameObject itemPrefab;

    public float despawnDistance;

    protected GameManager gameManager;
    protected EnemyStats stats;
    protected Rigidbody2D playerRb2d;
    protected Vector2 moveDirection;

    protected override void Start()
    {
        base.Start();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        stats = GetComponent<EnemyStats>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        StartCoroutine(DecideMoveDirection());
        StartCoroutine(ShootPlayer());
    }

    void Update()
    {
        if (stats.Health <= 0)
        {
            gameManager.Score += 100;
            Instantiate(itemPrefab, rb2d.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if ((rb2d.position - playerRb2d.position).magnitude >= despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveDirection * stats.MoveSpeed;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
        rb2d.rotation = angle;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player Projectile")
        {
            stats.Health -= col.GetComponent<PlayerProjectileController>().Damage;
        }
    }

    protected virtual IEnumerator DecideMoveDirection()
    {
        while (true)
        {
            float horizontal = Random.Range(-1f, 1f);
            float vertical = Random.Range(-1f, 1f);
            moveDirection = new Vector2(horizontal, vertical).normalized;

            yield return new WaitForSeconds(stats.MoveInterval);
        }
    }

    protected virtual IEnumerator ShootPlayer()
    {
        while (true)
        {
            for (int i = 0; i < stats.ShootBurstNum; i++)
            {
                for (int j = -(stats.ShootLineNum / 2); j <= stats.ShootLineNum / 2; j++)
                {
                    GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
                    Destroy(projectile, stats.ProjectileLifetime);

                    Vector2 playerDirection = playerRb2d.position - rb2d.position;
                    float angle = Mathf.Atan2(playerDirection.y, playerDirection.x);
                    float radius = playerDirection.magnitude;
                    angle += j / 5.0f;
                    Vector2 shootDirection = new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
                    projectile.GetComponent<EnemyProjectileController>().Initialize(stats.ProjectileSpeed, shootDirection, stats.ProjectileDamage);
                }

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(stats.ShootInterval);
        }
    }
}