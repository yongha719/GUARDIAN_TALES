using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string json = JsonUtility.ToJson(PlayerData); // ScriptableObject를 JSON 문자열로 변환
            string path = Path.Combine(Application.persistentDataPath, "data.json"); // 저장 경로

            File.WriteAllText(path, json); // JSON 파일로 저장
            print("저장 성공");
        }
    }
}
