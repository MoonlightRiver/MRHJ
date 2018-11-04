using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health;
    public float speed;
    public float shootElapsed;
    public float shootCoolDown;
    public float MaxbarrierMaintain;
    public float barrierCoolDown;
    public float MaxbarrierCoolDown;
    public bool isJumping;
    public GameObject projectilePrefab;

    private Rigidbody2D rb2d;

    void Start()
    {
        shootElapsed = shootCoolDown;
        barrierCoolDown = MaxbarrierCoolDown;
        isJumping = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void CheckGameOver()
    {
        if (this.health <= 0)
        {
            Debug.LogError("Game Over");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (!isJumping)
            {
                this.health -= 20;
                Debug.Log("피격. 남은 체력 : " + this.health);
            }
            else
            {
                Debug.Log("회피 성공.");
            }
            CheckGameOver();
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyProjectile")
        {
            if (!isJumping)
            {
                this.health -= 25;
                Debug.Log("피격. 남은 체력 : " + this.health);
            }
            else
            {
                Debug.Log("회피 성공.");
            }
            CheckGameOver();
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical);
        rb2d.velocity = direction * speed;

        float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        float angle = Mathf.Atan2(mousePosition.y - rb2d.position.y, mousePosition.x - rb2d.position.x) * Mathf.Rad2Deg + 90;
        rb2d.rotation = angle;

        barrierCoolDown += Time.deltaTime;

        if (Input.GetButtonDown("Jump")) //Space bar > Jump
        {
            if (barrierCoolDown >= MaxbarrierCoolDown) //Initial : 무적 1초 / 종료 후 쿨타임 시작 / 쿨 20초
            {
                Debug.Log("무적 사용.");
                barrierCoolDown = 0;
            }
            else
            {
                Debug.Log("Cooltime. Remain : " + (MaxbarrierCoolDown - barrierCoolDown));
            }
        }

        if (barrierCoolDown <= MaxbarrierMaintain)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    void Update()
    {
        shootElapsed += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (shootElapsed >= shootCoolDown)
            {
                shootElapsed = 0;
                GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);

                float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
                Vector2 mouseDirection = new Vector2(mousePosition.x - rb2d.position.x, mousePosition.y - rb2d.position.y);

                projectile.GetComponent<ProjectileController>().Direction = mouseDirection;
            }
        }
    }
}
