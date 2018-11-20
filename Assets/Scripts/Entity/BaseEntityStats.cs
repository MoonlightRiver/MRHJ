using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEntityStats : MonoBehaviour
{
    public Text healthText;

    public int initialMaxHealth;

    private int _health;

    public int MaxHealth { get; set; }
    public virtual int Health {
        get {
            return _health;
        }
        set {
            _health = Mathf.Min(value, MaxHealth);

            healthText.text = string.Format("HP: {0}/{1}", Health, MaxHealth);
        }
    }

    protected virtual void Start()
    {
        MaxHealth = initialMaxHealth;
        Health = initialMaxHealth;
    }
}
