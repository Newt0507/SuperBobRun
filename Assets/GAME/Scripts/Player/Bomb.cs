using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.BeingHit(transform);
                ObjectPoolManager.Instance.Return(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.Return(gameObject);
    }
}
