using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private WaveSystem _waveSystem;
    [SerializeField]
    private TextMeshPro _waveDisplay;
    private int _currentWave = 0;

    private bool _startDissapear = false;

    void OnEnable()
    {
        AppearWave();
    }

    void OnDisable()
    {
        _startDissapear = false;
        _waveSystem.ActiveSpawn();
    }

    private void Update() 
    {
        if(_startDissapear && _waveDisplay.color.a > 0f)
        {
            var color = _waveDisplay.color;
            color.a -= Time.deltaTime;
            _waveDisplay.color = color;
        } 
        else if (_startDissapear && _waveDisplay.color.a <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator StartDisplay()
    {
        yield return new WaitForSeconds(3f);
        _waveDisplay.text = "START";
        StartCoroutine(StartDissapearing());
    }

    IEnumerator StartDissapearing()
    {
        yield return new WaitForSeconds(2f);
        _startDissapear = true;
    }

    private void AppearWave()
    {
        _currentWave++;
        _waveDisplay.text = $"WAVE {_currentWave}";
        var color = _waveDisplay.color;
        color.a = 1f;
        _waveDisplay.color = color;

        StartCoroutine(StartDisplay());
    }
}
