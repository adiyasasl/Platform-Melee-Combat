using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimations : MonoBehaviour
{
    [Header("Properties")]
    //[SerializeField]
    //private UnityEvent _onEnemyAttack;
    [SerializeField]
    private EnemyBehaviour _enemyBehaviour;
    [SerializeField]
    private EnemyAttack _enemyAttack;
    [SerializeField]
    private Animator _anim;

    private PlayerAnimations _playerAnim;

    [HideInInspector]
    public bool _hasPlayer = false;
    private void Start()
    {
        _playerAnim = FindObjectOfType<PlayerAnimations>();
    }
    public void PlayAttack()
    {
        if (!_playerAnim._isDead)
        {
            _anim.SetBool("isWalking", false);
            _anim.SetTrigger("attack");
            _enemyBehaviour._isHit = true;
            _enemyBehaviour._isAttack = true;
            StopAllCoroutines();
            StartCoroutine(FalseIsHit());
        }
        else
            return;
    }
    public void PlayWalk()
    {
        _anim.SetBool("isWalking", true);
        _enemyBehaviour._isMoving = true;
    }
    public void PlayIdle()
    {
        _anim.SetBool("isWalking", false);
        _enemyBehaviour._isMoving = false;
    }
    public void PlayHit()
    {
        _anim.SetBool("isWalking", false);
        _anim.SetTrigger("hit");
        _enemyBehaviour._isHit = true;
        StopAllCoroutines();
        StartCoroutine(FalseIsHit());
    }
    public void PlayDead()
    {
        _anim.SetBool("isWalking", false);
        _anim.SetTrigger("death");
        _enemyBehaviour._isHit = true;
    }
    public IEnumerator FalseIsHit()
    {
        yield return new WaitForSeconds(1f);
        _enemyBehaviour._isHit = false;
        _enemyBehaviour._isAttack = false;
        _enemyAttack._canAttack = true;

        if (_hasPlayer)
        {
            _enemyAttack.Attack();
        }
    }

    public void AttackPlayer()
    {
        if (_hasPlayer)
            _playerAnim.GetHit();
        else
            return;
    }
}
