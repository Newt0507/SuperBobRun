using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shell : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _timeGetBackToSnail;
    [SerializeField] private Transform _snail;
    

    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    
    private float _direction;
    
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _timeGetBackToSnail -= Time.deltaTime;

        if (_timeGetBackToSnail <= 0)
        {
            Instantiate(_snail, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        _rigid.velocity = new Vector2(_direction * _moveSpeed * Time.deltaTime, _rigid.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ReversedDirection"))
        {
            _direction = -_direction;
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.layer != LayerMask.NameToLayer("Shell"))
            {
                gameObject.layer = LayerMask.NameToLayer("Shell");
                _direction = (transform.position - other.transform.position).normalized.x;
                
                if (_direction < 0)
                    _direction = -1;
                else if (_direction > 0)
                    _direction = 1;
            }
            else
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.TakeDamage(transform, damageAmount: 1);
                _direction = -_direction;
            }
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                enemy.BeingHit(transform);
        }
        

        
        
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _direction > 0;
    }

    // private void OnBecameInvisible()
    // {
    //     enabled = false;
    // }
    //
    // private void OnDisable()
    // {
    //     Destroy(gameObject);
    // }
}
