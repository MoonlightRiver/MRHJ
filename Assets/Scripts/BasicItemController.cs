using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasicItemType
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

public class BasicItemController : BaseItemController
{
    private static readonly Dictionary<BasicItemType, float> DropRate = new Dictionary<BasicItemType, float>()
    {
        {BasicItemType.Heal, 50},
        {BasicItemType.MaxHealthIncrease, 6},
        {BasicItemType.MoveSpeedIncrease, 6},
        {BasicItemType.ShootIntervalDecrease, 6},
        {BasicItemType.ProjectileDamageIncrease, 9},
        {BasicItemType.ProjectileSpeedIncrease, 7},
        {BasicItemType.JumpDurationIncrease, 8},
        {BasicItemType.JumpCooldownDecrease, 8}
    };
    private static readonly float SumDropRate = CalculateSumDropRate();
    private static float CalculateSumDropRate()
    {
        float sum = 0;
        foreach (KeyValuePair<BasicItemType, float> rate in DropRate)
        {
            sum += rate.Value;
        }
        return sum;
    }

    public BasicItemType Type { get; set; }

    void Start()
    {
        Type = ChooseRandomType();
        GetComponent<SpriteRenderer>().sprite = sprites[(int)Type];
    }

    private BasicItemType ChooseRandomType()
    {
        int length = System.Enum.GetNames(typeof(BasicItemType)).Length;

        float randomPoint = Random.Range(0f, SumDropRate);
        for (int i = 0; i < length; i++)
        {
            float rate = DropRate[(BasicItemType)i];
            if (randomPoint < rate)
                return (BasicItemType)i;
            else
                randomPoint -= rate;
        }
        return (BasicItemType)(length - 1);
    }
}
