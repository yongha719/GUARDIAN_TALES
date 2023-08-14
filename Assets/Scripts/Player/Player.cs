using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public float Speed;

    void FixedUpdate()
    {
        transform.Translate(PlayerUI.moveDirection * (Speed * Time.deltaTime));
    }
}