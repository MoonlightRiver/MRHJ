using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossStats : BaseEntityStats
{
    public float initialMoveSpeed;

    public float MoveSpeed { get; set; }

    protected override void Start()
    {
        base.Start();

        MoveSpeed = initialMoveSpeed;
    }
}
