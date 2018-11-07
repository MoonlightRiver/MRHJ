using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public float health;
    public float speed;
    public float lifespan;

    private GameObject gameManager;
    private Rigidbody2D rb2d;
    private float secondsElapsed;
    //private float secondsElapsed3;
    private Vector2 direction;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        rb2d = GetComponent<Rigidbody2D>();
        secondsElapsed = 0;
        //secondsElapsed3 = 0;

        float horizontal = Random.Range(-1f, 1f);
        float vertical = Random.Range(-1f, 1f);

        Vector2 value = new Vector2(horizontal, vertical);
        direction = value.normalized;

        rb2d.velocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb2d.rotation = angle;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            health -= 60;
            gameManager.GetComponent<GameManager>().Score += 30;
        }
    }

    void Update()
    {
        secondsElapsed += Time.deltaTime;
        //secondsElapsed3 += Time.deltaTime;

        if (secondsElapsed >= lifespan)
        {
            Destroy(gameObject);
        }

        //if (secondsElapsed3 >= 0.8)
        //{
        //    secondsElapsed3 = 0;
        //    GameObject enemyprojectile = Instantiate(enemyprojectilePrefab, rb2d.position, Quaternion.identity);

        //    Vector2 shootDirection = playerPosition - enemyPosition;

        //    enemyprojectile.GetComponent<EnemyProjectileController>().Direction = shootDirection;
        //}

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
