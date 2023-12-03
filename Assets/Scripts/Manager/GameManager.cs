using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUARDIANTALES
{

    public class GameManager : Singleton<GameManager>
    {
        protected override bool DontDestroy => true;

        public GuardianData GuardianData;

        public List<Enemy> Enemies = new();

        private List<Guardian> guardianParty = new();

        public IList<Guardian> GuardianParty => guardianParty;

        protected override void Awake()
        {
            base.Awake();

            GuardianData.Init();
        }

        private void Start()
        {
            var @int = 10;
            @int.eq();

            print(@int);
        }


        public Enemy GetNearestEnemy()
        {
            if (Enemies.Count == 0)
                return null;

            return Enemies.OrderBy(enemy => Vector3.Distance(GuardianData.Guardian.transform.position, enemy.transform.position)).First();
        }

        public Vector3 GetNearestEnemyPosition()
        {
            return GetNearestEnemy().transform.position;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                string json = JsonUtility.ToJson(GuardianData);
                string path = Path.Combine(Application.persistentDataPath, "data.json");

                File.WriteAllText(path, json);
                print("저장 성공");
            }
        }
    }
}
