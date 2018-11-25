using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : BaseEntityController
{
    public GameObject projectilePrefab;

    public float despawnDistance;
    public float despawnAfter;

    private GameManager gameManager;
    private Rigidbody2D playerRb2d;
    private FoodStats stats;

    protected override void Start()
    {
        base.Start();

        stats = GetComponent<FoodStats>();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        StartCoroutine(Shoot());

        Destroy(gameObject, despawnAfter);
    }

    void Update()
    {
        if (stats.HitCount >= 10)
        {
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
            stats.HitCount++;
            gameManager.Score += 30;
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
                    Destroy(projectile, stats.ProjectileLifetime);

                    float angle = 2 * Mathf.PI * j / 8;
                    Vector2 shootDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    projectile.GetComponent<EnemyProjectileController>().Initialize(stats.ProjectileSpeed, shootDirection, stats.ProjectileDamage);
                }

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(5);
        }
    }
}
