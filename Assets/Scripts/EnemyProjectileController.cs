using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour {

    public float speed;
    public float lifespan;

    private Rigidbody2D rb2d;
    private float secondsElapsed;

    private Vector2 direction;
    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value.normalized;
        }
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        secondsElapsed = 0;
    }

    void FixedUpdate()
    {
        rb2d.velocity = Direction * speed;
    }

    void Update()
    {
        secondsElapsed += Time.deltaTime;
        if (secondsElapsed >= lifespan)
        {
            Destroy(gameObject);
        }
    }
}
