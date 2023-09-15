using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public abstract class Entity : MonoBehaviour
{
    [Header(nameof(Entity)),SerializeField]
    private int bbracjji;

    public abstract EntityData Data { get; set; }


    protected virtual void Start()
    {
    }


    public virtual void Hit(int damage, ElementalAttribute enemyElemental)
    {
        Data.Hp.Value -= damage.ModifyDamage(Data.ElementalAttribute, enemyElemental);
    }
}