using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private EnemyAnimations _enemyAnim;
    [SerializeField]
    private EnemyHit _enemyHit;
    [SerializeField]
    private float _delayAttack = 1f;

    public bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _enemyAnim._hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _enemyAnim._hasPlayer = false;
        }
    }
    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(_delayAttack);
        if(!_enemyHit._isDead && _canAttack)
        {
            _canAttack = false;
            _enemyAnim.PlayAttack();
        }
    }

    public void Attack()
    {
        StopAllCoroutines();
        StartCoroutine(StartAttack());
    }
}
