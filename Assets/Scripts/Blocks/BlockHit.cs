using System;
using System.Collections;
using System.Collections.Generic;
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
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_maxHit != 0 && other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.HitBlock(transform))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        _spriteRenderer.enabled = true;
        
        _maxHit--;
        if (_maxHit == 0)
        {
            _spriteRenderer.sprite = _emptyBlockSprite;
        }
        
        if (_coin != null)
            Instantiate(_coin, transform);
        
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
