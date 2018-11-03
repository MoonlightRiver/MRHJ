using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    public float lifespan;
    private float secondsElapsed;
    private float secondsElapsed2;
    private float secondsElapsed3;
    public GameObject projectilePrefab;

    private Rigidbody2D rb2d;
    private Vector2 direction;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        secondsElapsed = 0;
        secondsElapsed2 = 0;
        secondsElapsed3 = 0;
    }

    void Update() {
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

    }
}
