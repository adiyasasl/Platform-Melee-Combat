using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [Header("Component")]
    [SerializeField]
    private Rigidbody2D _rb;

    [Header("Properties")]
    [SerializeField]
    private float _maxForce = 20f;
    [SerializeField]
    private GameObject _playerScoreDisplay;

    public int _valueCoin = 1;
    private void Start()
    {
        float randomized = Random.Range(0f, 51f);
        float forceRandom = Random.Range(0f, _maxForce);
        float coinValueRandom = Random.Range(1f, _valueCoin);
        if(randomized % 2 < 1)
        {
            _rb.AddForce(Vector2.right * forceRandom, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(-Vector2.right * forceRandom, ForceMode2D.Impulse);
        }
        _rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _rb.gravityScale = 0;
        }

        if (collision.CompareTag("Player"))
        {
            Transform positionScore = collision.GetComponentInChildren<Transform>();
            GameManager.Instance.AddScore(_valueCoin);
            GameObject display = Instantiate(_playerScoreDisplay, positionScore);
            
            PlayerScoreDisplay scoreDisplay = display.GetComponent<PlayerScoreDisplay>();
            scoreDisplay.ModifyText(_valueCoin.ToString());
            Destroy(gameObject);
        }
    }
}
