﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject MiniBossPrefab;
    public GameObject foodPrefab;
    public GameObject redzonePrefab;
    public GameObject player;
    public Text timeText;
    public Text scoreText;
    public int timeScorePerSecond;
    public float enemySpawnRadiusFrom;
    public float enemySpawnRadiusTo;
    public float enemySpawnInterval;
    public float foodSpawnRadiusFrom;
    public float foodSpawnRadiusTo;
    public float foodSpawnInterval;
    public float redzoneCreateRadiusFrom;
    public float redzoneCreateRadiusTo;
    public float redzoneCreateInterval;
    public float MiniBossSpawnInterval;

    private Rigidbody2D playerRb2d;
    private Vector2 playerPosition;
    private int timeSeconds;
    public int TimeSeconds {
        get {
            return timeSeconds;
        }
        set {
            timeSeconds = value;
            timeText.text = timeText.text = string.Format("{0:d2}:{1:d2}", timeSeconds / 60, timeSeconds % 60);
        }
    }
    private int score;
    public int Score {
        get {
            return score;
        }
        set {
            score = value;
            scoreText.text = string.Format("{0:n0}", score);
        }
    }

    void Start()
    {
        playerRb2d = player.GetComponent<Rigidbody2D>();
        TimeSeconds = 0;
        Score = 0;

        StartCoroutine(UpdateTimeSeconds());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnMiniBoss());
        StartCoroutine(SpawnFood());
        StartCoroutine(CreateRedzone());
    }

    void Update()
    {
        playerPosition = playerRb2d.position;
    }

    private IEnumerator UpdateTimeSeconds()
    {
        while (true)
        {
            TimeSeconds += 1;
            Score += timeScorePerSecond;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float radius = Random.Range(enemySpawnRadiusFrom, enemySpawnRadiusTo);
            float angle = Random.Range(0f, 360f);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 spawnPosition = playerPosition + new Vector2(x, y);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    private IEnumerator SpawnMiniBoss()
    {
        while (true)
        {
            float radius = Random.Range(enemySpawnRadiusFrom, enemySpawnRadiusTo);
            float angle = Random.Range(0f, 360f);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 spawnPosition = playerPosition + new Vector2(x, y);

            Instantiate(MiniBossPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(MiniBossSpawnInterval);
        }
    }

    private IEnumerator SpawnFood()
    {
        while (true)
        {
            float radius = Random.Range(foodSpawnRadiusFrom, foodSpawnRadiusTo);
            float angle = Random.Range(0f, 360f);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 spawnPosition = playerPosition + new Vector2(x, y);

            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(foodSpawnInterval);
        }
    }

    private IEnumerator CreateRedzone()
    {
        while (true)
        {
            float radius = Random.Range(redzoneCreateRadiusFrom, redzoneCreateRadiusTo);
            float angle = Random.Range(0f, 360f);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 createPosition = playerPosition + new Vector2(x, y);

            Instantiate(redzonePrefab, createPosition, Quaternion.identity);

            yield return new WaitForSeconds(redzoneCreateInterval);
        }
    }
}
