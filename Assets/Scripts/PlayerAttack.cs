using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private PlayerAnimations _playerAnim;
    [SerializeField]
    private Transform _parentTransform;
    [SerializeField]
    private Transform _playerPosition;
    [SerializeField]
    private float _attackMove = 0.3f;
    [SerializeField]
    private List<IDamageable> _enemies = new List<IDamageable>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IDamageable>() != null)
        {
            _enemies.Add(collision.GetComponent<IDamageable>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageable>() != null)
        {
            _enemies.Remove(collision.GetComponent<IDamageable>());
        }
    }

    public void AttackEnemies()
    {
        if (!_playerAnim._isFlip)
            _parentTransform.position += Vector3.right * _attackMove;
        else
            _parentTransform.position -= Vector3.right * _attackMove;

        // foreach (IDamageable enemies in _enemies)
        // {
        //     if(enemies != null)
        //         enemies.Damage(_playerPosition.position);
        //     else
        //         continue;
        // }

        for(int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Damage(_playerPosition.position);
        }
    }
}
