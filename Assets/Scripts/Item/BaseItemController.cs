using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemController : MonoBehaviour
{
    public Sprite[] sprites;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
