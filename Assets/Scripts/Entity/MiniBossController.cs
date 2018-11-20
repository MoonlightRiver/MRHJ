using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossController : EnemyController
{
    protected override void Update()
    {
        if (stats.Health <= 0)
        {
            gameManager.Score += 500;
            Instantiate(itemPrefab, rb2d.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected override IEnumerator Move()
    {
        // Can be edited to produce behaviors different from enemy
        while (true)
        {
            float horizontal = Random.Range(-1f, 1f);
            float vertical = Random.Range(-1f, 1f);

            Vector2 direction = new Vector2(horizontal, vertical).normalized;

            rb2d.velocity = direction * stats.MoveSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rb2d.rotation = angle;

            yield return new WaitForSeconds(stats.MoveInterval);
        }
    }

    protected override IEnumerator ShootPlayer()
    {
        // Can be edited to produce behaviors different from enemy
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, rb2d.position, Quaternion.identity);
            Destroy(projectile, stats.ProjectileLifetime);

            Vector2 playerDirection = playerRb2d.position - rb2d.position;
            projectile.GetComponent<EnemyProjectileController>().Initialize(stats.ProjectileSpeed, playerDirection, stats.ProjectileDamage);

            yield return new WaitForSeconds(stats.ShootInterval);
        }
    }
}