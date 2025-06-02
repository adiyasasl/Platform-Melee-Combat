using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    [Header("Components")]
    private WaveSystem _waveSystem;

    [Header("Properties")]
    [SerializeField]
    private GameObject _shopPanel;

    [Header("Shop Panel 1")]
    [SerializeField]
    private TextMeshProUGUI _infoDipslay1;
    [SerializeField]
    private TextMeshProUGUI _priceDisplay1;
    [SerializeField]
    private int _priceValue1 = 200;

    [Header("Shop Panel 2")]
    [SerializeField]
    private TextMeshProUGUI _infoDipslay2;
    [SerializeField]
    private TextMeshProUGUI _priceDisplay2;
    [SerializeField]
    private int _priceValue2 = 200;

    private void Start() 
    {
        _waveSystem = FindObjectOfType<WaveSystem>();  
        _priceDisplay1.text = _priceValue1.ToString();
        _priceDisplay2.text = _priceValue2.ToString();
    }
    public void ChangePrice(int shopIndex, int price)
    {
        switch(shopIndex)
        {
            case 1:
                if(GameManager.Instance.Score >= _priceValue1)
                {
                    GameManager.Instance.DecreaseScore(_priceValue1);
                    _priceValue1 += price;
                    _priceDisplay1.text = _priceValue1.ToString();
                    _shopPanel.SetActive(false);
                }
                break;
            case 2:
                if(GameManager.Instance.Score >= _priceValue2)
                {
                    GameManager.Instance.DecreaseScore(_priceValue2);
                    _priceValue2 += price;
                    _priceDisplay2.text = _priceValue2.ToString();
                    _shopPanel.SetActive(false);
                }
                break;
        }
    }
}
