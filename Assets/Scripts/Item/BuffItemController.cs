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
    private static readonly float SumOfDropRate = CalculateSumOfDropRate();
    private static float CalculateSumOfDropRate()
    {
        float sum = 0;
        foreach (KeyValuePair<BuffItemType, float> rate in DropRate)
        {
            sum += rate.Value;
        }
        return sum;
    }

    public BuffItemType Type { get; private set; }
    public Sprite Sprite { get; private set; }

    void Start()
    {
        Type = ChooseRandomType();
        Sprite = sprites[(int)Type];
        GetComponent<SpriteRenderer>().sprite = Sprite;
    }

    private BuffItemType ChooseRandomType()
    {
        int length = System.Enum.GetNames(typeof(BuffItemType)).Length;

        float randomPoint = Random.Range(0f, SumOfDropRate);
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
