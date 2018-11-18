using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileController : MonoBehaviour
{
    public int Damage { get; set; }

    public void Initialize(float speed, Vector2 direction, int damage)
    {
        GetComponent<Rigidbody2D>().velocity = speed * direction.normalized;
        Damage = damage;
    }
}
