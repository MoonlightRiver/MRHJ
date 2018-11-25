using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodStats : BaseEntityStats
{
    public Text hitCountText;

    private int _hitCount;

    public int HitCount {
        get {
            return _hitCount;
        }
        set {
            _hitCount = value;

            hitCountText.text = "Hit: " + HitCount.ToString();
        }
    }

    protected override void Start()
    {
        HitCount = 0;
    }
}
