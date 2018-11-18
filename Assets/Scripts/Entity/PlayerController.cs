﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseEntityController
{
    public GameObject projectilePrefab;
    public Text jumpText;
    public Text panelJumpText;

    private PlayerStats stats;
    private float shootElapsed;
    private float jumpElapsed;
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

    protected override void Start()
    {
        base.Start();

        stats = GetComponent<PlayerStats>();

        shootElapsed = 0;

        jumpElapsed = float.PositiveInfinity;
        IsJumping = false;

        // Will be removed
        transformElapsed = 0.1f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite1;
    }

    void Update()
    {
        if (stats.Health <= 0)
        {
            // Game over
        }

        MoveAndRotate();
        Jump();
        Shoot();
    }

    private void MoveAndRotate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical);

        rb2d.velocity = stats.MoveSpeed / 60f * direction.normalized;

        float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        float angle = Mathf.Atan2(mousePosition.y - rb2d.position.y, mousePosition.x - rb2d.position.x) * Mathf.Rad2Deg + 90;

        rb2d.rotation = angle;

        // Will be removed
        if (!(horizontal == 0 && vertical == 0))
        {
            transformElapsed -= Time.deltaTime;
        }
        if (transformElapsed <= 0)
        {
            transformElapsed = 0.1f;
            Transform();
        }
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

                GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
                Destroy(projectile, stats.ProjectileLifetime);

                float cameraDistance = Camera.main.transform.position.z - gameObject.transform.position.z;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
                Vector2 mouseDirection = mousePosition - rb2d.position;

                projectile.GetComponent<PlayerProjectileController>().Initialize(stats.ProjectileSpeed, mouseDirection, stats.ProjectileDamage);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (!IsJumping)
            {
                stats.Health -= 20;
            }
            else
            {
                Debug.Log("회피 성공.");
            }
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy Projectile")
        {
            if (!IsJumping)
            {
                stats.Health -= 25;
            }
            else
            {
                Debug.Log("회피 성공.");
            }
        }
        else if (col.gameObject.tag == "Basic Item")
        {
            BasicItemController item = col.gameObject.GetComponent<BasicItemController>();
            stats.ApplyBasicItemEffect(item.Type);
        }
        else if (col.gameObject.tag == "Redzone")
        {
            stats.Health = 0; //Instant death
        }
    }

    // Will be removed
    public float transformElapsed;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;

    private SpriteRenderer spriteRenderer;

    private void Transform()
    {
        if (spriteRenderer.sprite == Sprite1)
        {
            spriteRenderer.sprite = Sprite2;
        }
        else if (spriteRenderer.sprite == Sprite2)
        {
            spriteRenderer.sprite = Sprite3;
        }
        else if (spriteRenderer.sprite == Sprite3)
        {
            spriteRenderer.sprite = Sprite4;
        }
        else
        {
            spriteRenderer.sprite = Sprite1;
        }
    }
}
