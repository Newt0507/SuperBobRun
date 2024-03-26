using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    private const string IS_RUNNING = "IsRunning";
    private const string JUMP = "Jump";
    private const string FALL = "Fall";
    private const string HIT = "Hit";

    [Header("Health Properties")]
    [SerializeField] private int _health;
    [SerializeField] private float _damageAmount;
    
    [Space]
    [Header("Move Properties")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    //[SerializeField] private Vector3 _offset;
    [SerializeField] private float _bounceForce;

    [Space]
    [Header("Move Properties")]
    [SerializeField] private Transform _bullet;
    
    private PlayerControls _playerControls;
    private Rigidbody2D _rigid;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    
    private float _direction;
    private bool _isGrounded;
    private bool _previousFlip;
    private bool _isBeingHit;
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
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
        
        //float interactRadius = .5f;
        //_isGrounded = Physics2D.OverlapCircle(transform.position + _offset, interactRadius, _groundLayer);

        if (!_isBeingHit)
        {
            _rigid.velocity = new Vector2(_direction * _moveSpeed * Time.deltaTime, _rigid.velocity.y);

            _anim.SetBool(IS_RUNNING, _direction != 0);
        }
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

    private bool CanAttack(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        return Vector2.Dot(direction.normalized, Vector2.down) > 0.5f;
    }

    public void TakeDamage(Transform attackerTransform , int damageAmount)
    {
        _anim.SetTrigger(HIT);
        _health -= damageAmount;

        Vector2 bounceDirection = (transform.position - attackerTransform.position).normalized;
        _rigid.velocity = new Vector2(bounceDirection.x * _bounceForce, _bounceForce);
        
        if (_health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            _rigid.gravityScale += Time.deltaTime;
            Destroy(gameObject, 2f);
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (CanAttack(enemy.transform))
            {
                enemy.BeingHit();
            }
            else
            {
                _isBeingHit = true;
                TakeDamage(enemy.transform, enemy.GetDamageAmount());
            }
        }
        else
        {
            _isBeingHit = false;
        }
    }

    public int GetPlayerHealth()
    {
        return _health;
    }
}
