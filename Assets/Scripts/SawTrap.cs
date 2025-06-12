using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private BoxCollider2D _collider;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var damageAble = collision.GetComponent<IDamageable>();

        if (damageAble != null)
        {
            //damageAble.Damage()
        }
    }
}
