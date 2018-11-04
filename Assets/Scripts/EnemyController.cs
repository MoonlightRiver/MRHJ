﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float speed;
    public float lifespan;
    public GameObject enemyprojectilePrefab;
    //public Text scoreText;

    private float secondsElapsed;
    private float secondsElapsed2;
    private float secondsElapsed3;
    private Rigidbody2D playerRb2d;
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private Vector2 enemyPosition;
    private Vector2 playerPosition;

    void Start()
    {
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        rb2d = GetComponent<Rigidbody2D>();
        secondsElapsed = 0;
        secondsElapsed2 = 0;
        secondsElapsed3 = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            this.health -= 60;
        }
    }

    void Update()
    {
        enemyPosition = rb2d.position;
        playerPosition = playerRb2d.position;

        secondsElapsed += Time.deltaTime;
        secondsElapsed2 += Time.deltaTime;
        secondsElapsed3 += Time.deltaTime;

        if (secondsElapsed >= lifespan)
        {
            Destroy(gameObject);
        }

        if (secondsElapsed2 >= 0.5)
        {
            secondsElapsed2 = 0;
            float horizontal = Random.Range(-1, 1);
            float vertical = Random.Range(-1, 1);

            Vector2 value = new Vector2(horizontal, vertical);
            direction = value.normalized;
            if (horizontal < 0) direction.x = -direction.x;
            if (vertical < 0) direction.y = -direction.y;

            rb2d.velocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rb2d.rotation = angle;
        }

        if (secondsElapsed3 >= 0.8)
        {
            secondsElapsed3 = 0;
            GameObject enemyprojectile = Instantiate(enemyprojectilePrefab, rb2d.position, Quaternion.identity);

            Vector2 shootDirection = playerPosition - enemyPosition;

            enemyprojectile.GetComponent<EnemyProjectileController>().Direction = shootDirection;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            //int currentScore = int.Parse(scoreText.text.Replace(",", ""));
            //scoreText.text = string.Format("{0:#,###}", currentScore + 100);
        }
    }
}