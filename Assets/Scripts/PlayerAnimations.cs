using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private HealthSystem _playerHealth;

    [SerializeField]
    private float _attackDelay = 0.35f;
    [SerializeField]
    private float _rollDelay = 2f;

    private int _attackValue;
    private float _attackTime = 0f;
    private float _rollTime = 0f;

    public bool _isDead = false;
    public bool _isFlip = false;
    public bool _isRoll = false;
    public bool _canRoll = false;
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        _attackTime += Time.deltaTime;
        _rollTime += Time.deltaTime;
        // Toggle Mode if(((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && _attackValue <= 3 && _attackTime >= _attackDelay) && _playerMovement.IsGrounded && !_isDead && !_isRoll)
        if(((Input.GetMouseButtonDown(0) && _attackValue <= 3 && _attackTime >= _attackDelay) && _playerMovement.IsGrounded && !_isDead && !_isRoll))
        {
            Attack();
            _attackTime = 0f;
        }


        if (_playerMovement.IsMoving)
        {
            _animator.SetBool("isMoving", true);
            if (horizontal > 0f)
            {
                _isFlip = false;
                transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            }
            else if (horizontal < 0f)
            {
                _isFlip = true;
                transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            }
        } 
        else
            _animator.SetBool("isMoving", false);

        if (_playerMovement.IsGrounded)
            _animator.SetBool("isGrounded", true);
        else
            _animator.SetBool("isGrounded", false);

        if(Input.GetKeyDown(KeyCode.Space) && _playerMovement.IsGrounded && !_isDead)
            _animator.SetTrigger("jump");

        if (_rollTime >= _rollDelay)
            _canRoll = true;
        else
            _canRoll = false;

        if (Input.GetMouseButtonDown(1) && !_isDead && !_isRoll && _canRoll)
        {
            _isRoll = true;
            _rollTime = 0f;
            _animator.SetTrigger("roll");
            _playerMovement.Roll(_isFlip);
            _attackValue = 0;
        }

        _animator.SetFloat("yAxis", _playerMovement.Velocity().y);

    }

    private void Attack()
    {
        if (_attackValue == 3)
            return;
            //ResetAttackValue();
        else
            _attackValue++;

        _animator.SetTrigger("attack"+_attackValue);
        _playerMovement.StopPlayer(false);
    }

    public void GetHit()
    {
        if (!_isDead && !_isRoll)
        {
            _animator.SetTrigger("hit");
            _playerMovement.StopPlayer(false);
        }
        
        if(!_isRoll)
            CheckHealth();
    }

    private void CheckHealth()
    {
        _playerHealth.DecreaseHealth(1);
        
        if(_playerHealth._healthValue <= 0 && !_isDead)
        {
            _isDead = true;
            _animator.SetTrigger("death");
            _playerMovement.StopPlayer(true);
            enabled = false;
        }
    }

    public void ResetAttackValue()
    {
        _attackValue = 0;
        _attackTime = _attackDelay;
        _playerMovement.enabled = true;
        _playerMovement._canMove = true;
    }

    public void ResetRollValue()
    {
        _isRoll = false;
        _playerMovement.enabled = true;
        _playerMovement._canMove = true;
    }
}
