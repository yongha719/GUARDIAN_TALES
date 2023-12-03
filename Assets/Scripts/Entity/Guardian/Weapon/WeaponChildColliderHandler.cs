using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUARDIANTALES
{
    public class WeaponChildColliderHandler : MonoBehaviour
    {
        private Weapon weapon;

        void Start()
        {
            weapon = GetComponentInParent<Weapon>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            weapon.OnChildTriggerEnter2D(other);
        }
    }
}
