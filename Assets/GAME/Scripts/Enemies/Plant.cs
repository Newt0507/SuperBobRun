using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plant : Enemy
{
    private const string ATTACK = "Attack";
    
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private float _firedTime;
    [SerializeField] private float _firedForce;
    [SerializeField] private Sprite _deathSprite;

    private Transform _player;
    private Vector2 _direction;
    private float _previousDirection = 1f;
    private float _lastFireTime;
    private bool _isBeingHit;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    public override void Move()
    {
        if (_player != null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _direction = (_player.transform.position - transform.position).normalized;
        
        if (_previousDirection != _direction.x && _direction.x != 0f)
            _previousDirection = _direction.x;

        _spriteRenderer.flipX = _previousDirection > 0;
    }
    
    public override void Attack()
    {
        if (_player != null)
        {
            _anim.SetTrigger(ATTACK);
            if(!_isBeingHit)
                Fired();
        }
    }

    private void UpdateFireTime()
    {
        _lastFireTime = Time.time;
    }
    
    private void Fired()
    {
        if (Time.time >= _lastFireTime + _firedTime)
        {
            ObjectPoolManager.Instance.Get("Bullet", _fireTransform).GetComponent<Rigidbody2D>()
                .AddForce(Vector2.right * _previousDirection * _firedForce, ForceMode2D.Impulse);
            UpdateFireTime();
        }
    }
    
    public override void BeingHit(Transform transform)
    {
        _isBeingHit = true;
        _anim.enabled = false;
        _spriteRenderer.sprite = _deathSprite;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }
}
