using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garam : Guardian
{
    protected override void Attack()
    {
        Weapon.Attack();
    }

    protected override void Start()
    {
        base.Start();
    }
}
