using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mushroom : Enemy
{
    [SerializeField] private Sprite _deathSprite;
    
    private int _direction;

    private void Start()
    {
        _direction = -1; // move left
        Flip();
    }
    
    public override void Move()
    {
        _rigid.velocity =  new Vector2(_direction * _moveSpeed * Time.deltaTime, _rigid.velocity.y);
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

    public override void BeingHit()
    {
        _direction = 0;
        _anim.enabled = false;
        _spriteRenderer.sprite = _deathSprite;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }
    
    
    
}
