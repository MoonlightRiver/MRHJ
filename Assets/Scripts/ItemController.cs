using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heal,
    MaxHealthIncrease,
    MoveSpeedIncrease,
    ShootIntervalDecrease,
    ProjectileDamageIncrease,
    ProjectileSpeedIncrease,
    JumpDurationIncrease,
    JumpCooldownDecrease
}

public class ItemController : MonoBehaviour
{
    private static readonly Dictionary<ItemType, float> ItemDropRate = new Dictionary<ItemType, float>()
    {
        {ItemType.Heal, 50},
        {ItemType.MaxHealthIncrease, 6},
        {ItemType.MoveSpeedIncrease, 6},
        {ItemType.ShootIntervalDecrease, 6},
        {ItemType.ProjectileDamageIncrease, 9},
        {ItemType.ProjectileSpeedIncrease, 7},
        {ItemType.JumpDurationIncrease, 8},
        {ItemType.JumpCooldownDecrease, 8}
    };
    private static readonly float SumItemDropRate = CalculateSumItemDropRate();
    private static float CalculateSumItemDropRate()
    {
        float sum = 0;
        foreach (KeyValuePair<ItemType, float> rate in ItemDropRate)
        {
            sum += rate.Value;
        }
        return sum;
    }

    public Sprite[] sprites;

    public ItemType Type { get; set; }

    void Start()
    {
        Type = ChooseRandomType();
        GetComponent<SpriteRenderer>().sprite = sprites[(int)Type];
    }

    private ItemType ChooseRandomType()
    {
        int length = System.Enum.GetNames(typeof(ItemType)).Length;

        float randomPoint = Random.Range(0f, SumItemDropRate);
        for (int i = 0; i < length; i++)
        {
            float rate = ItemDropRate[(ItemType)i];
            if (randomPoint < rate)
                return (ItemType)i;
            else
                randomPoint -= rate;
        }
        return (ItemType)(length - 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
