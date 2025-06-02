using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Properties")]
    [SerializeField]
    private TextMeshProUGUI _scoreDisplay;
    [SerializeField]
    private int _score = 0;

    [Header("Events")]
    [SerializeField]
    private UnityEvent _onScoreIncreased;

    public int Score => _score;
    private void Awake()
    {
        Instance = this;
    }

    private void Start() 
    {
        _scoreDisplay.text = _score.ToString();
    }

    public void AddScore(int value)
    {
        if (_onScoreIncreased != null)
            _onScoreIncreased.Invoke();
        
        _score += value;
        _scoreDisplay.text = _score.ToString();
    }

    public void DecreaseScore(int value)
    {
        _score -= value;
        _scoreDisplay.text = _score.ToString();
    }
}
