using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEntityController : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    private RectTransform canvasRectTransform;

    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        canvasRectTransform = GetComponentInChildren<RectTransform>();
    }

    void LateUpdate()
    {
        canvasRectTransform.rotation = Quaternion.identity;
    }
}
