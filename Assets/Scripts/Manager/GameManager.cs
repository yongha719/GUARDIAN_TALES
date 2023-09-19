using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    protected override bool DontDestroy => true;

    public GuardianData GuardianData;

    public List<Enemy> Enemies = new();


    protected override void Awake()
    {
        base.Awake();

        GuardianData.Init();
    }

    public Enemy GetnearestEnemy()
    {
        if(Enemies.Count == 0)
            return null;

        return Enemies.OrderBy(enemy => Vector3.Distance(GuardianData.Guardian.transform.position, enemy.transform.position)).First();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            string json = JsonUtility.ToJson(GuardianData); // ScriptableObject를 JSON 문자열로 변환
            string path = Path.Combine(Application.persistentDataPath, "data.json"); // 저장 경로

            File.WriteAllText(path, json); // JSON 파일로 저장
            print("저장 성공");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            print(GetnearestEnemy().name);
        }
    }
}
