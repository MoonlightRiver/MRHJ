using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffItemType
{
    AllSpeedIncrease,
    MoveSpeedIncrease,
    ShootIntervalDecrease,
    ProjectileDamageIncrease,
    ProjectileSpeedIncrease,
    Jump
}

public class BuffItemController : BaseItemController
{
    private static readonly Dictionary<BuffItemType, float> DropRate = new Dictionary<BuffItemType, float>()
    {
        {BuffItemType.AllSpeedIncrease, 1},
        {BuffItemType.MoveSpeedIncrease, 1},
        {BuffItemType.ShootIntervalDecrease, 1},
        {BuffItemType.ProjectileDamageIncrease, 1},
        {BuffItemType.ProjectileSpeedIncrease, 1},
        {BuffItemType.Jump, 1},
    };
    private static readonly float SumDropRate = CalculateSumDropRate();
    private static float CalculateSumDropRate()
    {
        float sum = 0;
        foreach (KeyValuePair<BuffItemType, float> rate in DropRate)
        {
            sum += rate.Value;
        }
        return sum;
    }

    public BuffItemType Type { get; set; }

    void Start()
    {
        Type = ChooseRandomType();
        GetComponent<SpriteRenderer>().sprite = sprites[(int)Type];
    }

    private BuffItemType ChooseRandomType()
    {
        int length = System.Enum.GetNames(typeof(BuffItemType)).Length;

        float randomPoint = Random.Range(0f, SumDropRate);
        for (int i = 0; i < length; i++)
        {
            float rate = DropRate[(BuffItemType)i];
            if (randomPoint < rate)
                return (BuffItemType)i;
            else
                randomPoint -= rate;
        }
        return (BuffItemType)(length - 1);
    }
}
