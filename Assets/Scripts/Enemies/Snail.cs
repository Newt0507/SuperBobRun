using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snail : Enemy
{
    [SerializeField] private Sprite _shellSprite;
    [SerializeField] private float _dragForce;

    private int _direction;
    private bool isShell;

    private void Start()
    {
        _direction = -1; // move left
        Flip();
    }

    public override void Move()
    {
        if(!isShell)
            _rigid.velocity = new Vector2(_direction * _moveSpeed * Time.deltaTime, _rigid.velocity.y);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _direction > 0;
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
        if (other.gameObject.CompareTag("Player") && isShell)
        {
            Vector2 direction = (transform.position - other.gameObject.transform.position).normalized;
            _rigid.AddForce(direction * _dragForce, ForceMode2D.Impulse);
            Destroy(gameObject, 1.5f);
        }
    }

    public override void BeingHit()
    {
        isShell = true;
        _direction = 0;
        _anim.enabled = false;
        _spriteRenderer.sprite = _shellSprite;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

}
