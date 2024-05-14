using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class BlockHit : MonoBehaviour
{
    //[SerializeField] private Transform _coin;
    //[SerializeField] private Transform _heart;
    [SerializeField] private int _maxHit = -1;
    [SerializeField] private Sprite _emptyBlockSprite;
    [SerializeField] private bool _canBreak;
    [SerializeField] private bool _isMisteryBlock;

    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private bool _isHitting;
    
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
            _spriteRenderer.enabled = true;

            if (player.HitBlock(transform))
            {
                _isHitting = true;
                Hit();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_isHitting && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.BeingHit(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isHitting = false;
    }

    private void Hit()
    {
        _maxHit--;
        if (_maxHit == 0)
            _spriteRenderer.sprite = _emptyBlockSprite;

        if (_isMisteryBlock)
        {
            int random = Random.Range(0, 5);

            switch (random)
            {
                case 0 or 1:
                    AudioManager.Instance.PlaySFX(ESound.Health);
                    ObjectPoolManager.Instance.Get("Heart", transform);
                    //Instantiate(_heart, transform);
                    break;
                case 2 or 3:
                    AudioManager.Instance.PlaySFX(ESound.Coin);
                    ObjectPoolManager.Instance.Get("Coin", transform);
                    //Instantiate(_coin, transform);
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
