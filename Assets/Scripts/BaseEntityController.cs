using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEntityController : MonoBehaviour
{
    public Text healthText;
    public int startHealth;
    public float speed;
    public float despawnDistance;

    protected Rigidbody2D rb2d;
    private RectTransform canvasRectTransform;
    private int health;
    public int Health {
        get {
            return health;
        }
        protected set {
            health = value;
            healthText.text = "HP: " + health;
        }
    }

    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        canvasRectTransform = GetComponentInChildren<RectTransform>();

        Health = startHealth;
    }

    void LateUpdate()
    {
        canvasRectTransform.rotation = Quaternion.identity;
    }
}
