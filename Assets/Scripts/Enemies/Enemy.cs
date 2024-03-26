using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _damageAmount;

    protected Animator _anim;
    protected Rigidbody2D _rigid;
    protected SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        enabled = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        //Debug.LogError("Enemy.Move();");
    }

    public virtual void BeingHit()
    {
        //Debug.LogError("Enemy.BeingHit();");
    }
    // public virtual void Attack()
    // {
    //     
    // }

    public int GetDamageAmount()
    {
        return _damageAmount;
    }
    
    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnEnable()
    {
        _rigid.WakeUp();
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnDisable()
    {
        _rigid.velocity = Vector2.zero;
        _rigid.Sleep();
    }
}
