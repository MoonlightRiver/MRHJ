using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    private float timeElapsed;

    public GameObject player;
    public Transform Destination;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            player.transform.position = Destination.transform.position;
        }
    }

    private void Rotate()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 0.5) timeElapsed = 0;

        rb2d.rotation = timeElapsed * -1080;
    }
}