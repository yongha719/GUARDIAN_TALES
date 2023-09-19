using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;


public class PoolManager : Singleton<PoolManager>
{
    protected override bool DontDestroy => true;

    protected override void Awake()
    {
        base.Awake();
    }
}
