using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject foodPrefab;
    public GameObject player;
    public Text timeText;
    public Text scoreText;
    public float enemySpawnRadiusFrom;
    public float enemySpawnRadiusTo;
    public float enemySpawnInterval;
    public float foodSpawnRadiusFrom;
    public float foodSpawnRadiusTo;
    public float foodSpawnInterval;

    private Rigidbody2D playerRb2d;
    private Vector2 playerPosition;
    private float secondsElapsed;
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
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnFood());
        secondsElapsed = 0;
        Score = 0;
    }

    void Update()
    {
        playerPosition = playerRb2d.position;

        secondsElapsed += Time.deltaTime;
        timeText.text = string.Format("{0:d2}:{1:d2}", (int)secondsElapsed / 60, (int)secondsElapsed % 60);
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

    private IEnumerator DeleteEnemy()
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

    private IEnumerator DeleteFood()
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
}
