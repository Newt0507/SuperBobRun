using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public static Player Instance { get; private set; }
    
    public static event EventHandler OnHealthChanged;
    
    private const string IS_RUNNING = "IsRunning";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_FALLING = "IsFalling";
    private const string IS_BEING_HIT = "IsBeingHit";

    [Header("Health Properties")]
    [SerializeField] private int _maxHealth;
    
    [Space]
    [Header("Move Properties")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _bounceForce;

    [Space]
    [Header("Attack Properties")]
    [SerializeField] private int _coinValueToFire;
    [SerializeField] private Transform _bomb;
    [SerializeField] private float _firedForce;

    
    private PlayerControls _playerControls;
    private Rigidbody2D _rigid;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private int _health = -1;
    private float _direction;
    private bool _isGrounded;
    private float _previousDirection = 1f;
    private bool _isJumping;
    private bool _isFalling;
    private bool _isBeingHit;
    private bool _isAttacking;
    
    private void Awake()
    {
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        
        _playerControls = new PlayerControls();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _health = _maxHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        _playerControls.Enable();

        _playerControls.Player.Move.performed += ctx => { _direction = ctx.ReadValue<float>(); };
        _playerControls.Player.Jump.performed += ctx => Jump();
        _playerControls.Player.Fired.performed += ctx => Fire();

        
    }

    private void Update()
    {
        Flip();
        JumpAndFallAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float jumpDistance = .55f;
        _isGrounded = Physics2D.BoxCast(transform.position, transform.lossyScale / 2, 0,
              Vector2.down, jumpDistance, _groundLayer);

         float moveDistance = .15f;
         if (Physics2D.BoxCast(transform.position, transform.lossyScale / 2, 0,
                 new Vector2(_direction, 0), moveDistance, _groundLayer))
         {
             _direction = 0;
         }
         
        if (!_isBeingHit)
        {
            _rigid.velocity = new Vector2(_direction * _moveSpeed * Time.deltaTime, _rigid.velocity.y);

            _anim.SetBool(IS_RUNNING, _direction != 0);
        }
    }

    private void OnDrawGizmos()
    {
        float maxDistance = .15f;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.lossyScale / 2, 0,
                                 Vector2.right, maxDistance, _groundLayer) ;
        if (hit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
        
        
    }

    private void Flip()
    {
        if (_previousDirection != _direction && _direction != 0f)
            _previousDirection = _direction;
        
        _spriteRenderer.flipX = _previousDirection < 0;
    }
    
    private void Jump()
    {
        if (_isGrounded)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _isJumping = true;
        }
    }

    private void Fire()
    {
        if (GameManager.Instance.GetCoin() >= _coinValueToFire && _health > 0)
        {
            Transform spell = Instantiate(_bomb, transform);
            spell.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _previousDirection * _firedForce, ForceMode2D.Impulse);

            GameManager.Instance.SetCoin(-_coinValueToFire);
        }        
    }

    
    private void JumpAndFallAnimation()
    {
        if (_isGrounded)
            _isFalling = false;
        else
        {
            _isJumping = false;
            if (_rigid.velocity.y < 0)
                _isFalling = true;
        }
        

        _anim.SetBool(IS_JUMPING, _isJumping);
        _anim.SetBool(IS_FALLING, _isFalling);
    }

    private bool DotTest(Transform target, Vector2 testVector, float amount)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        return Vector2.Dot(direction.normalized, testVector) > amount;
    }

    private bool CanAttack(Transform target)
    {
        _isAttacking = true;
        return DotTest(target, Vector2.down, 0.5f);
    }

    public bool HitBlock(Transform block)
    {
        return DotTest(block, Vector2.up, 0f);
    }

    public void TakeDamage(Transform attackerTransform , int damageAmount)
    {
        _anim.SetTrigger(IS_BEING_HIT);
        _health -= damageAmount;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);

        Bounce(attackerTransform);
        
        if (_health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            _rigid.gravityScale += Time.deltaTime;

            GameManager.Instance._isGameOver = true;
        }
    }

    private void Bounce(Transform fromTransform)
    {
        Vector2 bounceDirection = (transform.position - fromTransform.position).normalized;
        _rigid.velocity = new Vector2(bounceDirection.x * _bounceForce, _bounceForce);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
            if(enemy != null)
            {
                if (CanAttack(enemy.transform))
                {
                    enemy.BeingHit(transform);
                    Bounce(enemy.transform);
                    _isAttacking = false;
                }
                else
                {
                    _isBeingHit = true;
                    TakeDamage(enemy.transform, enemy.GetDamageAmount());
                }
            }
        }
        else
        {
            _isBeingHit = false;
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int healthValue)
    {
        _health += healthValue;
        
        if (_health >= _maxHealth)
            _health = _maxHealth;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public bool GetAttackState()
    {
        return _isAttacking;
    }
    
    public int GetCostFireValue()
    {
        return _coinValueToFire;
    }

    private void OnBecameInvisible()
    {
        OnHealthChanged = null;
        GameManager.Instance._isGameOver = true;
        enabled = false;
    }

    private void OnDisable()
    {
        //Instance = null;
        _playerControls.Disable();
        Destroy(gameObject);
    }
}
