using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEntityStats : MonoBehaviour
{
    public Text healthText;

    public int initialMaxHealth;
    public float initialMovementSpeed;

    private int _health;
    public virtual int Health {
        get {
            return _health;
        }
        set {
            _health = value;
            healthText.text = string.Format("HP: {0}/{1}", _health, MaxHealth);
        }
    }
    public int MaxHealth { get; set; }
    public virtual float MovementSpeed { get; set; }

    protected virtual void Start()
    {
        MaxHealth = initialMaxHealth;
        Health = initialMaxHealth;
        MovementSpeed = initialMovementSpeed;
    }
}
