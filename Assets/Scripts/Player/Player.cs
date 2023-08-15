using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public PlayerData PlayerData;

    private void Start()
    {
        PlayerData = GameManager.Instance.PlayerData;
    }

    void FixedUpdate()
    {
        // Move
        transform.Translate(PlayerUIManager.MoveDirection * (PlayerData.Speed.Value * Time.deltaTime));

        // Attack
    }


    private void OnDrawGizmos()
    {
    }
}