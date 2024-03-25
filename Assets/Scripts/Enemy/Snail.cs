using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _range = 3f;

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
        //_anim.SetBool(IS_WALKING, _isRunning);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     string layer = LayerMask.LayerToName(other.gameObject.layer);
    //     if (layer == "Ground")
    //     {
    //         _moveRight = !_moveRight;
    //     }
    // }
}
