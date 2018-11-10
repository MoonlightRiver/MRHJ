using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseEntityController
{
    public GameObject projectilePrefab;
    public float moveInterval;
    public float shootPlayerInterval;
    public float projectileLifetime;

    private GameManager gameManager;
    private Rigidbody2D playerRb2d;

    new void Start()
    {
        base.Start();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        StartCoroutine(Move());
        StartCoroutine(ShootPlayer());
    }

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            gameManager.Score += 100;
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
            Health -= 60;
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
}