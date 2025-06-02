using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private PlayerAnimations _playerAnim;
    [Header("Properties")]
    [SerializeField]
    private SpriteRenderer _rollIcon;

    private void Update() 
    {
        if(_playerAnim._canRoll)
            ChangeRollIcon(true);
        else
            ChangeRollIcon(false);
    }

    private void ChangeRollIcon(bool condition)
    {
        Color color = _rollIcon.color;

        if(condition)
        {
            color.a = 1f;
        }
        else
        {
            color.a = .4f;
        }

        _rollIcon.color = color;
    }
}
