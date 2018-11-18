using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossController : BaseEntityController
{
    public GameObject projectilePrefab;
    public GameObject itemPrefab;

    private GameManager gameManager;
    private MiniBossStats stats;
    private Rigidbody2D playerRb2d;

    protected override void Start()
    {
        base.Start();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        stats = GetComponent<MiniBossStats>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        StartCoroutine(Move());
        StartCoroutine(ShootPlayer());
    }

    void Update()
    {
        if (stats.Health <= 0)
        {
            gameManager.Score += 500;
            Instantiate(itemPrefab, rb2d.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player Projectile")
        {
            stats.Health -= col.GetComponent<PlayerProjectileController>().Damage;
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            float horizontal = Random.Range(-1f, 1f);
            float vertical = Random.Range(-1f, 1f);

            Vector2 direction = new Vector2(horizontal, vertical).normalized;

            rb2d.velocity = direction * stats.MoveSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rb2d.rotation = angle;

            yield return new WaitForSeconds(stats.MoveInterval);
        }
    }

    private IEnumerator ShootPlayer()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
            Destroy(projectile, stats.ProjectileLifetime);

            Vector2 playerDirection = playerRb2d.position - rb2d.position;
            projectile.GetComponent<EnemyProjectileController>().Initialize(stats.ProjectileSpeed, playerDirection, stats.ProjectileDamage);

            yield return new WaitForSeconds(stats.ShootInterval);
        }
    }
}