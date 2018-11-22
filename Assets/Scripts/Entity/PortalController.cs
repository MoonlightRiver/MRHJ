using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    protected Rigidbody2D rb2d;
    private float timeElapsed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 0.5) timeElapsed = 0;

        rb2d.rotation = timeElapsed * -1080;
    }
}