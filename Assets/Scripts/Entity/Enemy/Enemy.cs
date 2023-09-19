using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public override EntityData Data { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [Header(nameof(Enemy))]
    [SerializeField]
    protected EnemyData guardianData;
}
