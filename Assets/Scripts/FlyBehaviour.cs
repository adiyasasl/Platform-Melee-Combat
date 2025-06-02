using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField]
    private EnemyAnimations _enemyAnim;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _enemyTransform;
    [SerializeField]
    private float _forceSpeed = 5f;
    [SerializeField]
    private float _durationForce = 1.5f;

    private bool _isTowardPlayer = false;

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (_isTowardPlayer)
        {
            BeginAttack();
        }
    }
    public void BeginAttack()
    {
        Vector2 pos = (_playerTransform.position - _enemyTransform.position).normalized;
        //_rb.MovePosition(_playerTransform.position);
        _rb.AddForce(new Vector2(pos.x, 0f) * _forceSpeed, ForceMode2D.Force);
    }
    public void StartAttack()
    {
        _isTowardPlayer = true;
        StopAllCoroutines();
        StartCoroutine(DelayForce());
    }

    IEnumerator DelayForce()
    {
        yield return new WaitForSeconds(_durationForce);
        _isTowardPlayer = false;
    }
}
