using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour, IDamageable
{
    [Header("Properties")]
    [SerializeField]
    private HealthSystem _enemyHealth;
    [SerializeField]
    private EnemyAnimations _enemyAnim;
    [SerializeField]
    private EnemyBehaviour _enemyBehaviour;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _force = 10f;

    public bool _isDead = false;
    private void Start() { }

    public void Damage(Vector2 direction)
    {
        if (direction.x > transform.position.x)
            _rb.AddForce(-Vector2.right * _force);
        else if (direction.x < transform.position.x)
            _rb.AddForce(Vector2.right * _force);

        if(!_enemyBehaviour._isAttack)
            _enemyHealth.DecreaseHealth(1);

        if (_enemyHealth._healthValue <= 0 && !_isDead)
        {
            _isDead = true;
            Die();
        }
        else if (_enemyHealth._healthValue > 0 && !_enemyBehaviour._isAttack)
        {
            _enemyAnim.PlayHit();
        }
    }

    public void Die()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePosition;
        _enemyAnim.PlayDead();
    }
}
