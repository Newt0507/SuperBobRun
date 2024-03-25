using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private const string IS_RUNNING = "IsRunning";

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _range = 3f;
    [SerializeField] private LayerMask _enemyLayer;

    private Animator _anim;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _startPos;
    private bool _isRunning;
    private bool _moveRight;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        CheckMovement();
        Animation();

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckMovement()
    {
        float minRange = _startPos.x - _range;
        float maxRange = _startPos.x + _range;
        
        if (transform.position.x <= minRange)
        {
            _moveRight = true;
            if(Math.Abs(transform.position.x - minRange) <= 0.1f)
                _spriteRenderer.flipX = true;
        }
        else if (transform.position.x >= maxRange)
        {
            _moveRight = false;
            if (Math.Abs(transform.position.x - maxRange) <= 0.1f)
                _spriteRenderer.flipX = false;
        }
    }
    
    private void Move()
    {
        if (_moveRight)
            _rigid.velocity = Vector2.right * _moveSpeed;
        else
            _rigid.velocity = Vector2.left * _moveSpeed;
    }


    private void Animation()
    {
        //_anim.SetBool(IS_RUNNING, _isRunning);
    }

}
