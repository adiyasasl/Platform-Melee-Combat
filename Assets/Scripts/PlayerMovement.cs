using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _jumpForce = 3f;
    [SerializeField]
    private float _rollForce = 5f;

    private bool _isGrounded;
    private bool _isMoving;

    [HideInInspector]
    public bool _canMove = true;

    public bool IsMoving => _isMoving;
    public bool IsGrounded => _isGrounded;
    private void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal != 0f)
            _isMoving = true;
        else
            _isMoving = false;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _canMove)
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

        if(_canMove)
            _rb.velocity = new Vector2(Horizontal * _speed, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    public void StopPlayer(bool isDead)
    {
        _canMove = false;
        _rb.velocity = Vector2.zero;

        if (isDead)
            Destroy(this);
            //enabled = false;
    }

    public void Roll(bool flip)
    {
        _canMove = false;

        if (flip && _isGrounded)
        {
            _rb.velocity = new Vector2(-_rollForce, _rb.velocity.y);
        }
        else if (!flip && _isGrounded)
        {
            _rb.velocity = new Vector2(_rollForce, _rb.velocity.y);
        }
        else
            return;
    }

    public Vector2 Velocity()
    {
        return _rb.velocity;
    }
}
