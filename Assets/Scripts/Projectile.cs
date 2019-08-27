
using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage;

    public void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
