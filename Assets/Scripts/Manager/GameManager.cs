using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override bool DontDestroy => true;

    public PlayerData PlayerData;

    protected override void Awake()
    {
        base.Awake();
        
        PlayerData.Init();
    }
}
