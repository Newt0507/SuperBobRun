using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class BlockHit : MonoBehaviour
{
    [SerializeField] private Transform _coin;
    [SerializeField] private int _maxHit = -1;
    [SerializeField] private Sprite _emptyBlockSprite;
    [SerializeField] private bool _canBreak;

    private Animator _anim;
    private Player _player;
    private SpriteRenderer _spriteRenderer;

    private bool _isHit;
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_maxHit != 0 && other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            _spriteRenderer.enabled = true;

            if (player.HitBlock(transform))
            {
                _isHit = true;
                Hit();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_canBreak && _isHit && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.BeingHit(transform);
        }
    }

    private void Hit()
    {
        _maxHit--;
        if (_maxHit == 0)
            _spriteRenderer.sprite = _emptyBlockSprite;

        if (_coin != null)
        {
            int random = Random.Range(0, 5);

            switch (random)
            {
                case 0 or 1:
                    _player.SetHealth(_player.GetHealth());
                    break;
                case 2 or 3:
                    Instantiate(_coin, transform);
                    break;
                case 4:
                    break;
            }
        }
        
        StartCoroutine(Aminate());

    }

    private IEnumerator Aminate()
    {
        Vector2 restingPosition = transform.localPosition;
        Vector2 aminatingPosition = restingPosition + Vector2.up * .5f;

        yield return Move(restingPosition, aminatingPosition);

        if (_spriteRenderer.sprite == _emptyBlockSprite && _canBreak)
        {
            _anim.enabled = true;
            Destroy(gameObject, .2f);
        }
        
        yield return Move(aminatingPosition, restingPosition);
        
    }

    private IEnumerator Move(Vector2 fromPosition, Vector2 toPosition)
    {
        float elapsed = 0f;
        float duration = 0.15f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector2.Lerp(fromPosition, toPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = toPosition;
    }
    
}
