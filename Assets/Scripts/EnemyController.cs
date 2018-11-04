﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float health;
    public float speed;
    public float lifespan;
    private float secondsElapsed;
    private float secondsElapsed2;
    private float secondsElapsed3;
    public GameObject enemyprojectilePrefab;
    public GameObject player;

    private Rigidbody2D playerRb2d;
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private Vector2 enemyPosition;
    private Vector2 playerPosition;

    void Start()
    {
        playerRb2d = player.GetComponent<Rigidbody2D>();
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

    void Update() {

        enemyPosition = new Vector2(rb2d.position.x, rb2d.position.y);
        float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
        Vector3 playerLocation = Camera.main.ScreenToWorldPoint(new Vector3(playerRb2d.position.x, playerRb2d.position.y, cameraDistance));
        //보정값 x+8.888889, y+5
        playerPosition = new Vector2(playerLocation.x + 8.888889f, playerLocation.y + 5f);

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

        //현재 (0,0)으로만 발사됨. playerPosition이 작동하지 않고 있다.
        if (secondsElapsed3 >= 0.8)
        {
            secondsElapsed3 = 0;
            GameObject enemyprojectile = Instantiate(enemyprojectilePrefab, rb2d.position, Quaternion.identity);

            Debug.Log("Enemy : " + enemyPosition.x + ", " + enemyPosition.y);
            Debug.Log("Player : " + playerPosition.x + ", " + playerPosition.y);

            Vector2 shootDirection = new Vector2(playerPosition.x - enemyPosition.x, playerPosition.y - enemyPosition.y);

            enemyprojectile.GetComponent<EnemyProjectileController>().Direction = shootDirection;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}