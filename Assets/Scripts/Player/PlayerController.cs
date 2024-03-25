using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class PlayerController : MonoBehaviour
{
    private const string IS_RUNNING = "IsRunning";
    
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    [SerializeField] private LayerMask _groundLayer;
    
    private Animator _anim;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;

    private bool _isRunning;
    private bool _isGrounded;
    private Vector3 _movement;

    private float _moveHorizontal;
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInput();
        Animation();
    }

    private void FixedUpdate()
    {
        Jump();
        if(_isRunning) Move();
    }

    private void GetInput()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");

        _movement = new Vector3(_moveHorizontal, 0f);
        
        if (_movement != Vector3.zero)
        {
            _isRunning = true;
            
            Flip();

        }
        else
        {
            _isRunning = false;
        }
    }

    private void Move()
    {
        _rigid.velocity = _movement * _moveSpeed ;
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _moveHorizontal < 0f;
    }

    private void Animation()
    {
        _anim.SetBool(IS_RUNNING, _isRunning);
    }

    private void Jump()
    {
        float interactDistance = .5f;
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, interactDistance, _groundLayer);
        
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
    }

}
