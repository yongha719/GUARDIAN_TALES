using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public override EntityData Data { get => enemyData; set => enemyData = (EnemyData)value; }

    [Header(nameof(Enemy))]
    [SerializeField]
    protected EnemyData enemyData;
}
