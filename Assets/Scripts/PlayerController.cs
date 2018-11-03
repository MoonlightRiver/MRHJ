using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public GameObject projectilePrefab;

    private Rigidbody2D rb2d;

	void Start() {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);

        rb2d.velocity = movement * speed;

        float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));

        float angle = Mathf.Atan2(mouse.y - rb2d.position.y, mouse.x - rb2d.position.x) * Mathf.Rad2Deg + 90;
        rb2d.rotation = angle;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);

            float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
            Vector2 direction = new Vector2(mouse.x - rb2d.position.x, mouse.y - rb2d.position.y);

            projectile.GetComponent<ProjectileController>().Direction = direction;
        }
    }
}
