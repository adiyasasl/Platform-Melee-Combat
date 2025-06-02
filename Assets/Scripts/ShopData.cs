using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopData : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private ShopSystem _shopSystem;
    
    [Header("Properties")]
    [SerializeField]
    private int _index = 1;
    [SerializeField]
    private UnityEvent _onPlayerBuyProduct;

    public void BuyProduct()
    {
        _shopSystem.ChangePrice(_index, 50);
    }
}
