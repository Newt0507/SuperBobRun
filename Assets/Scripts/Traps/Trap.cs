using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int damageAmount = 1;
            other.gameObject.GetComponent<Player>().TakeDamage(transform, damageAmount);
        }
    }
}
