using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEntityStats : MonoBehaviour
{
    public Text healthText;
    public Text panelHealthText;
    public Text panelMovementSpeedText;

    public int initialHealth;
    public int initialMaxHealth;
    public float initialMovementSpeed;
    public float despawnDistance;

    private int _health;
    public int Health {
        get {
            return _health;
        }
        set {
            _health = value;
            healthText.text = string.Format("HP: {0}/{1}", _health, MaxHealth);
            panelHealthText.text = string.Format("HP: {0}/{1}", _health, MaxHealth);
        }
    }
    public int MaxHealth { get; set; }
    private float _movementSpeed;
    public float MovementSpeed {
        get {
            return _movementSpeed;
        }
        set {
            _movementSpeed = value;
            panelMovementSpeedText.text = _movementSpeed.ToString() + " px/s";
        }
    }

    void Start()
    {
        MaxHealth = initialMaxHealth;
        Health = initialHealth;
    }
}
