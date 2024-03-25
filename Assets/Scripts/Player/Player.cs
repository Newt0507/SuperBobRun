using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const string IS_RUNNING = "IsRunning";
    private const string JUMP = "Jump";
    private const string FALL = "Fall";
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    
    private PlayerControls _playerControls;
    private Rigidbody2D _rigid;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    
    private float _direction = 0f;
    private bool _isGrounded;
    private bool _previousFlip;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerControls.Enable();

        _playerControls.Player.Move.performed += ctx => { _direction = ctx.ReadValue<float>(); };
        _playerControls.Player.Jump.performed += ctx => Jump();

        _previousFlip = _spriteRenderer.flipX;
    }

    private void Update()
    {
        Flip();
        Fall();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float interactDistance = .5f;
        _isGrounded = Physics2D.Raycast(transform.position,
            Vector2.down, interactDistance, _groundLayer);
        
        _rigid.velocity = new Vector2(_direction * _moveSpeed * Time.deltaTime, _rigid.velocity.y);

        _anim.SetBool(IS_RUNNING, _direction != 0);
    }

    private void Flip()
    {
        if (_direction == 0f)
            _spriteRenderer.flipX = _previousFlip;
        else
        {
            _spriteRenderer.flipX = _direction < 0;
            _previousFlip = _spriteRenderer.flipX;
        }
    }
    
    private void Jump()
    {
        if (_isGrounded)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _anim.SetTrigger(JUMP);
        }
    }

    private void Fall()
    {
        if(!_isGrounded && _rigid.velocity.y < 0)
            _anim.SetTrigger(FALL);
    }

}
