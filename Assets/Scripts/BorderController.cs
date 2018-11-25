using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy Projectile")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Player Projectile")
        {
            Destroy(col.gameObject);
        }
    }
}
