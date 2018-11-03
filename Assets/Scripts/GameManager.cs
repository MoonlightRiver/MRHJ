using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject player;

    private Rigidbody2D playerRb2d;
    private Vector2 playerPosition;
    
    void Start() {
        playerRb2d = player.GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnEnemy());
    }
    
    void Update() {
        playerPosition = playerRb2d.position;
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
