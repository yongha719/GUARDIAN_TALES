using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerData PlayerData;

    protected override bool DontDestroy => true;

    protected override void Awake()
    {
        PlayerData.Init();
    }
}
