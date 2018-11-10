﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : BaseEntityController
{
    private GameManager gameManager;
    private Rigidbody2D playerRb2d;

    new void Start()
    {
        base.Start();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Health <= 0)
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
            Health -= 60;
            gameManager.Score += 30;
        }
    }
}
