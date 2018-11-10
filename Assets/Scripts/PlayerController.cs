using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseEntityController
{
    public GameObject projectilePrefab;
    public float jumpMaintain;
    public float jumpCooldown;
    public float projectileLifetime;
    public float shootCooldown;

    private float jumpElapsed;
    private bool isJumping;
    private float shootElapsed;

    new void Start()
    {
        base.Start();

        jumpElapsed = jumpCooldown;
        isJumping = false;

        shootElapsed = shootCooldown;
    }

    void Update()
    {
        MoveAndRotate();
        Jump();
        Shoot();
    }

    private void MoveAndRotate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical);

        rb2d.velocity = speed * direction.normalized;

        float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        float angle = Mathf.Atan2(mousePosition.y - rb2d.position.y, mousePosition.x - rb2d.position.x) * Mathf.Rad2Deg + 90;

        rb2d.rotation = angle;
    }

    private void Jump()
    {
        jumpElapsed += Time.deltaTime;

        if (Input.GetButtonDown("Jump")) //Space bar > Jump
        {
            if (jumpElapsed >= jumpCooldown) //Initial : 무적 1초 / 종료 후 쿨타임 시작 / 쿨 20초
            {
                Debug.Log("무적 사용.");
                jumpElapsed = 0;
            }
            else
            {
                Debug.Log("Cooltime. Remain : " + (jumpCooldown - jumpElapsed));
            }
        }

        if (jumpElapsed <= jumpMaintain)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    private void Shoot()
    {
        shootElapsed += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (shootElapsed >= shootCooldown)
            {
                shootElapsed = 0;

                GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
                Destroy(projectile, projectileLifetime);

                float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
                Vector2 mouseDirection = mousePosition - rb2d.position;

                projectile.GetComponent<PlayerProjectileController>().SetDirection(mouseDirection);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (!isJumping)
            {
                Health -= 20;
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
        if (col.gameObject.tag == "Enemy Projectile")
        {
            if (!isJumping)
            {
                Health -= 25;
            }
            else
            {
                Debug.Log("회피 성공.");
            }
            CheckGameOver();
        }
    }

    private void CheckGameOver()
    {
        if (Health <= 0)
        {
            Debug.LogError("Game Over");
        }
    }
}
