using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Properties")]
    public int _healthValue = 3;

    [Header("UI Element")]
    [SerializeField]
    private float _delayBar = 1f;
    [SerializeField]
    private float _speedBarDec = 4f;
    [SerializeField]
    private Slider _healthBarBackground;
    [SerializeField]
    private Slider _healthBarFill;

    private bool _isBgHealthDec = false;
    private bool _isFillHealthDec = false;
    private void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        if (_isFillHealthDec && !_isBgHealthDec)
        {
            if(_healthBarFill.value != 0 && _healthBarFill.value >= _healthValue)
            {
                //_healthBarFill.value -= Time.deltaTime * _speedBarDec;
                _healthBarFill.value = _healthValue;
                StopAllCoroutines();
                StartCoroutine(DecreaseBGHealth());
            }
            else
            {
            }
        }
        else if (_isBgHealthDec)
        {
            if (_healthBarBackground.value >= _healthBarFill.value)
            {
                _healthBarBackground.value -= Time.deltaTime * _speedBarDec;
            }
            else
            {
                _isBgHealthDec = false;
            }

            if (_healthBarBackground.value == 0)
                enabled = false;
        }
    }

    private void UpdateHealthBar()
    {
        _healthBarBackground.maxValue = _healthValue;
        _healthBarBackground.value = _healthValue;

        _healthBarFill.maxValue = _healthValue;
        _healthBarFill.value = _healthValue;
    }

    public void DecreaseHealth(int hitValue)
    {
        _healthValue -= hitValue;

        _isFillHealthDec = true;
    }

    public void IncreaseHealth()
    {
        _healthValue = (int)_healthBarFill.maxValue;

        UpdateHealthBar();
    }

    public void UpgradeValue(int healthValue)
    {
        _healthValue += healthValue;

        UpdateHealthBar();
        IncreaseHealth();
    }

    private IEnumerator DecreaseBGHealth()
    {
        _isFillHealthDec = false;
        yield return new WaitForSeconds(_delayBar);
        _isBgHealthDec = true;
    }
}
