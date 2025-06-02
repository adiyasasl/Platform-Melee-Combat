using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehaviour : MonoBehaviour, IDamageable
{
    [Header("Components")]
    [SerializeField]
    private BoxCollider2D _collider;
    [SerializeField]
    private Animator _anim;
    
    [Header("Properties")]
    [SerializeField]
    private bool _canAppear = true;
    
    public bool CanAppear => _canAppear;
    public void Damage(Vector2 direction)
    {
        if(_collider.enabled)
        {
            _anim.SetTrigger("isHit");
            _collider.enabled = false;
            _canAppear = true;
            DummySystem.Instance.RemoveAppearObj(this);
        }
    }

    public void Appear()
    {
        _anim.SetTrigger("isAppear");
        _collider.enabled = true;
        _canAppear = false;
    }

    public void Dissapear()
    {
        _anim.SetTrigger("isDisappear");
        _collider.enabled = false;
        _canAppear = true;
    }

    public void Die()
    {
        //throw new System.NotImplementedException();
    }
}
