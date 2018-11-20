﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject foodPrefab;
    public GameObject miniBossPrefab;
    public GameObject redzonePrefab;
    public Text timeText;
    public Text scoreText;
    public Text waveText;

    public int timeScorePerSecond;
    public float spawnRadiusFrom;
    public float spawnRadiusTo;
    public float enemySpawnInterval;
    public float foodSpawnInterval;
    public float miniBossSpawnInterval;
    public float redzoneCreateInterval;

    public int wave;

    private int _timeSeconds;
    private int _score;

    public int TimeSeconds {
        get {
            return _timeSeconds;
        }
        set {
            _timeSeconds = value;
            wave = 1 + (int)(_timeSeconds / 5);

            timeText.text = string.Format("{0:d2}:{1:d2}", TimeSeconds / 60, TimeSeconds % 60);
            waveText.text = "Wave " + wave.ToString();
        }
    }



    public int Score {
        get {
            return _score;
        }
        set {
            _score = value;

            scoreText.text = string.Format("{0:n0}", Score);
        }
    }

    private Rigidbody2D playerRb2d;
    private Vector2 playerPosition;

    void Start()
    {
        TimeSeconds = 0;
        Score = 0;
        playerRb2d = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

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
            float radius = Random.Range(spawnRadiusFrom, spawnRadiusTo);
            float angle = Random.Range(0f, 360f);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 spawnPosition = playerPosition + new Vector2(x, y);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    private IEnumerator SpawnFood()
    {
        while (true)
        {
            float radius = Random.Range(spawnRadiusFrom, spawnRadiusTo);
            float angle = Random.Range(0f, 360f);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 spawnPosition = playerPosition + new Vector2(x, y);

            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(foodSpawnInterval);
        }
    }

    private IEnumerator SpawnMiniBoss()
    {
        while (true)
        {
            float radius = Random.Range(spawnRadiusFrom, spawnRadiusTo);
            float angle = Random.Range(0f, 360f);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 spawnPosition = playerPosition + new Vector2(x, y);

            Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(miniBossSpawnInterval);
        }
    }

    private IEnumerator CreateRedzone()
    {
        yield return new WaitForSeconds(redzoneCreateInterval);

        while (true)
        {
            Instantiate(redzonePrefab, playerPosition, Quaternion.identity);

            yield return new WaitForSeconds(redzoneCreateInterval);
        }
    }
}
