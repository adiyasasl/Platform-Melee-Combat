using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private EnemyAnimations _enemyAnim;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private Transform _rayEnemyLocation;
    [SerializeField]
    private float _rangeRayPlayer = 10f;
    [SerializeField]
    private float _rangeRayEnemy = 2f;
    [SerializeField]
    private float _limitDistance = 5f;
    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private LayerMask _enemyLayer;

    private Transform _playerTransform;
    RaycastHit2D hitPlayer;
    RaycastHit2D hitEnemy;

    public bool _isFlyingEnemy = false;
    public bool _isMoving = true;
    public bool _isHit = false;
    public bool _isAttack = false;
    public bool _canMove = true;
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (transform.position.x > _playerTransform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0);
        }
        else if (transform.position.x < _playerTransform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 0, 0);
        }

        float distance = Vector2.Distance(transform.position, _playerTransform.position);

        if (distance > _limitDistance)
        {
            _canMove = true;
        }
        else
        {
            _canMove = false;
        }

        hitPlayer = Physics2D.Raycast(transform.position, transform.right, _rangeRayPlayer, _playerLayer);
        hitEnemy = Physics2D.Raycast(_rayEnemyLocation.position, transform.right, _rangeRayEnemy, _enemyLayer);
        CheckHitPlayer(hitPlayer);
        CheckHitEnemy(hitEnemy);

        if (_isMoving && !_isHit && _canMove && !_isFlyingEnemy)
            _rb.velocity = new Vector2(transform.right.x * _speed, _rb.velocity.y);
        else if (!_isHit && _isFlyingEnemy)
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _speed * Time.deltaTime);
        else
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
    }

    private void CheckHitPlayer(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") && !_isHit && _canMove)
            {
                _enemyAnim.PlayWalk();
            } 
            else if (!_canMove)
            {
                _enemyAnim.PlayIdle();
            }
        }
        else
        {
            _enemyAnim.PlayIdle();
        }
    }

    private void CheckHitEnemy(RaycastHit2D hit)
    {
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                _canMove = false;
                _enemyAnim.PlayIdle();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _canMove = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * _rangeRayPlayer);
    }
}
