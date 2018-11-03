using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject player;
    public Text timeText;

    private Rigidbody2D playerRb2d;
    private Vector2 playerPosition;
    private float secondsElapsed;
    
    void Start() {
        playerRb2d = player.GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnEnemy());
        secondsElapsed = 0;
    }
    
    void Update() {
        playerPosition = playerRb2d.position;
        secondsElapsed += Time.deltaTime;
        timeText.text = string.Format("{0:d2}:{1:d2}", (int)secondsElapsed / 60, (int)secondsElapsed % 60);
    }

    private IEnumerator SpawnEnemy() {
        while (true) {
            float radius = Random.Range(2.5f, 5);
            float angle = Random.Range(0, 360);
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            Vector2 startPosition = playerPosition + new Vector2(x, y);

            Instantiate(enemyPrefab, startPosition, Quaternion.identity);

            yield return new WaitForSeconds(3);
        }
    }
}
