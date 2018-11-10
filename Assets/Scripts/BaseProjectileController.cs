using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileController : MonoBehaviour
{
    public float speed;

    public void SetDirection(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = speed * direction.normalized;
    }
}
