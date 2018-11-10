using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyProjectilePrefab;
    public int startHealth;
    public float speed;
    public float despawnDistance;
    public float moveInterval;
    public float shootPlayerInterval;
    public float projectileLifetime;

    private GameManager gameManager;
    private Rigidbody2D rb2d;
    private Rigidbody2D playerRb2d;
    private int health;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        rb2d = GetComponent<Rigidbody2D>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        health = startHealth;

        StartCoroutine(Move());
        StartCoroutine(ShootPlayer());
    }

    void Update()
    {
        if (health <= 0)
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
            health -= 60;
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
            GameObject projectile = Instantiate(enemyProjectilePrefab, rb2d.position, Quaternion.identity);
            Destroy(projectile, projectileLifetime);

            Vector2 playerDirection = playerRb2d.position - rb2d.position;
            projectile.GetComponent<EnemyProjectileController>().SetDirection(playerDirection);

            yield return new WaitForSeconds(shootPlayerInterval);
        }
    }
}