using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEntityStats : MonoBehaviour
{
    public Text healthText;

    public int initialMaxHealth;

    private int _health;

    public virtual int Health {
        get {
            return _health;
        }
        set {
            _health = value;

            healthText.text = string.Format("HP: {0}/{1}", Health, MaxHealth);
        }
    }
    public int MaxHealth { get; set; }

    protected virtual void Start()
    {
        MaxHealth = initialMaxHealth;
        Health = initialMaxHealth;
    }
}
