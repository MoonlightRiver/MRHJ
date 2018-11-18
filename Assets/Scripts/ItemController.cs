using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heal,
    ProjectileSpeedIncrease,
    ProjectileCooldownDecrease,
    ProjectileDamageIncrease,
    MovementSpeedIncrease,
    JumpDurationIncrease,
    JumpCooldownDecrease,
    MaxHealthIncrease
}

public class ItemController : MonoBehaviour
{
    public string BSType;
    public ItemType type;
    public BuffType BfType;
    //public BossType BoType;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
