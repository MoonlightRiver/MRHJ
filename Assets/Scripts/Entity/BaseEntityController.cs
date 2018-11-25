using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEntityController : MonoBehaviour
{
    protected GameManager gameManager;
    protected Rigidbody2D rb2d;
    private RectTransform canvasRectTransform;

    protected virtual void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        rb2d = GetComponent<Rigidbody2D>();
        canvasRectTransform = GetComponentInChildren<RectTransform>();
    }

    void LateUpdate()
    {
        canvasRectTransform.rotation = Quaternion.identity;
    }
}
