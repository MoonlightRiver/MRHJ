using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : BaseEntityController
{
    public float despawnDistance;

    private GameManager gameManager;
    private Rigidbody2D playerRb2d;
    private FoodStats stats;

    protected override void Start()
    {
        base.Start();

        stats = GetComponent<FoodStats>();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (stats.Health <= 0)
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
            stats.Health -= col.GetComponent<PlayerProjectileController>().Damage;
            gameManager.Score += 30;
        }
    }
}
