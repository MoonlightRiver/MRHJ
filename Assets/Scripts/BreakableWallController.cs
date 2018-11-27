using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallController : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    private int health = 1000;
	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite1;
    }
	
	// Update is called once per frame
	void Update () {
        if(health >= 750)
        {
            spriteRenderer.sprite = Sprite1;
        }
        else if (health >= 500)
        {
            spriteRenderer.sprite = Sprite2;
        }
        else if (health >= 250)
        {
            spriteRenderer.sprite = Sprite3;
        }
        else
        {
            spriteRenderer.sprite = Sprite4;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy Projectile")
        {
            health -= col.GetComponent<EnemyProjectileController>().Damage;
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Player Projectile")
        {
            health -= col.GetComponent<PlayerProjectileController>().Damage;
            Destroy(col.gameObject);
        }
    }
}
