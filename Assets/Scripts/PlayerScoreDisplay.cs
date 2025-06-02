using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreDisplay : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private TextMeshPro _text;

    public void ModifyText(string text)
    {
        _text.text = $"+{text}";
    }
}
