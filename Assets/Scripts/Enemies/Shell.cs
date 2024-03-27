using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private float _throwForce;

    private Rigidbody2D _rigid;
    
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Shell");
            Vector2 direction = new Vector2((transform.position - other.gameObject.transform.position).normalized.x, 0);
            _rigid.AddForce(direction * _throwForce, ForceMode2D.Impulse);
        }

        if (_rigid.velocity != Vector2.zero && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.BeingHit();
        }
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
