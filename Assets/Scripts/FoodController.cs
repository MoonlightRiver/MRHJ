using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public int startHealth;
    public float despawnDistance;

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
    }

    void Update()
    {
        if (health <= 0)
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
            health -= 60;
            gameManager.Score += 30;
        }
    }
}
