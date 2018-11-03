using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;

    private Vector2 direction;
    public Vector2 Direction {
        get {
            return direction;
        }
        set {
            direction = Vector3.Normalize(value);
        }
    }

	void Start() {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
        rb2d.velocity = Direction * speed;
    }
}
