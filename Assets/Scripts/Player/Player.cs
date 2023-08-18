using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private void Start()
    {
        playerData = GameManager.Instance.PlayerData;
    }

    void FixedUpdate()
    {
        // Move
        transform.Translate(playerData.MoveDirection.normalized * (playerData.Speed.Value * Time.deltaTime));

        // Attack
    }

    void Attack()
    {
        
    }
}