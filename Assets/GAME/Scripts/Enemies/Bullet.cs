using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(transform, damageAmount: 1);

            //ObjectPoolManager.Instance.Return(gameObject);
        }

        ObjectPoolManager.Instance.Return(gameObject);
    }

    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.Return(gameObject);
    }
}
