using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : BaseEntityController
{
    public GameObject projectilePrefab;
    public Text jumpText;
    public Text panelJumpText;

    private bool _isJumping;
    public bool IsJumping {
        get {
            return _isJumping;
        }
        set {
            _isJumping = value;

            float jumpRest = stats.JumpCooldown - jumpElapsed;
            if(jumpRest > 0)
            {
                jumpText.text = IsJumping ? "Jumping" : string.Format("Cooldown: {0:f1}", jumpRest);
                panelJumpText.text = IsJumping ? string.Format("Jumping : {0:f1}", stats.JumpDuration - jumpElapsed) : string.Format("Cooldown: {0:f1}", jumpRest);
            }
            else
            {
                jumpText.text = "Ready";
                panelJumpText.text = "Ready";
            }
        }
    }

    private PlayerStats stats;
    private Animator animator;
    private Camera mainCamera;
    private float shootElapsed;
    private float jumpElapsed;
    private bool isMoving;

    protected override void Start()
    {
        base.Start();

        stats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        IsJumping = false;

        shootElapsed = 0;
        jumpElapsed = float.PositiveInfinity;
        isMoving = false;
    }

    void Update()
    {
        if (stats.Health <= 0 && !gameManager.IsDebugMode)
        {
            Invoke("GameOver", 3);
        }

        MoveAndRotate();
        Jump();
        Shoot();

        if (IsJumping)
        {
            animator.SetTrigger("playerJump");
        }
        else if (isMoving)
        {
            animator.SetTrigger("playerMove");
        }
        else
        {
            animator.SetTrigger("playerIdle");
        }
    }

    private void MoveAndRotate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical);

        rb2d.velocity = stats.MoveSpeed / 60f * direction.normalized;

        float cameraDistance = mainCamera.transform.position.z - gameObject.transform.position.z;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        float angle = Mathf.Atan2(mousePosition.y - rb2d.position.y, mousePosition.x - rb2d.position.x) * Mathf.Rad2Deg + 90;

        rb2d.rotation = angle;

        isMoving = !(horizontal == 0 && vertical == 0);
    }

    private void Jump()
    {
        jumpElapsed += Time.deltaTime;

        if (Input.GetButtonDown("Jump")) //Space bar > Jump
        {
            if (jumpElapsed >= stats.JumpCooldown) //Initial : 무적 1초 / 종료 후 쿨타임 시작 / 쿨 20초
            {
                jumpElapsed = 0;
            }
        }

        if (jumpElapsed <= stats.JumpDuration)
        {
            IsJumping = true;
        }
        else
        {
            IsJumping = false;
        }
    }

    private void Shoot()
    {
        shootElapsed += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (shootElapsed >= stats.ShootInterval)
            {
                shootElapsed = 0;

                float cameraDistance = mainCamera.transform.position.z - gameObject.transform.position.z;
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
                Vector2 mouseDirection = mousePosition - rb2d.position;
                Vector2 orthogonalDirection = new Vector2(mouseDirection.y, -mouseDirection.x);

                for (int i = -(stats.ShootLineNum / 2); i <= stats.ShootLineNum / 2; i++)
                {
                    Vector2 instantiatePosition = rb2d.position + i / 5f * orthogonalDirection.normalized;
                    GameObject projectile = Instantiate(projectilePrefab, instantiatePosition, Quaternion.identity);
                    Destroy(projectile, stats.ProjectileLifetime);
                    projectile.GetComponent<PlayerProjectileController>().Initialize(stats.ProjectileSpeed, mouseDirection, stats.ProjectileDamage);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (!IsJumping)
            {
                stats.Health -= 5;
            }
            else
            {
                Debug.Log("회피 성공.");
            }
            //Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy Projectile")
        {
            if (!IsJumping)
            {
                stats.Health -= col.GetComponent<EnemyProjectileController>().Damage;
            }
            else
            {
                Debug.Log("회피 성공.");
            }
        }
        else if (col.gameObject.tag == "Basic Item")
        {
            BasicItemController basicItem = col.GetComponent<BasicItemController>();
            stats.ApplyBasicItemEffect(basicItem);
        }
        else if (col.gameObject.tag == "Buff Item")
        {
            BuffItemController buffItem = col.GetComponent<BuffItemController>();
            stats.AddBuff(buffItem);
        }
        else if (col.gameObject.tag == "Redzone")
        {
            stats.Health = 0; //Instant death
        }
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("Score", gameManager.Score);
        SceneManager.LoadScene("Game Over");
    }
}
