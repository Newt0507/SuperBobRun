 using TMPro;
 using UnityEngine;

public class Snail : Enemy
{
    [SerializeField] private Transform _shell;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Sprite _deathSprite;
    
    private int _direction;

    private void Start()
    {
        _direction = -1; // move left
        Flip();
    }

    public override void Move()
    {
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
        float interactDistance = .5f;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.lossyScale / 4, 0,
            Vector2.right * _direction, interactDistance, _groundLayer);

        if (hit || _rigid.velocity == Vector2.zero)
        {
            _direction = -_direction;
            Flip();
        }
    }

    
    public override void BeingHit(Transform from)
    {
        Player player = from.gameObject.GetComponent<Player>();
        
        if (player!=null) //&& player.GetAttackState()
        {
            Instantiate(_shell, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            _direction = 0;
            _anim.enabled = false;
            _spriteRenderer.sprite = _deathSprite;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
