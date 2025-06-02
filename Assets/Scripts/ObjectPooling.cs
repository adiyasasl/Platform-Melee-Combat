using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private GameObject _object;
    [SerializeField]
    private int _amountPool = 10;
    [SerializeField]
    private List<GameObject> _objectPools = new List<GameObject>();
    void Start()
    {
        for(int i = 0; i < _amountPool; i++)
        {
            GameObject obj = Instantiate(_object, gameObject.transform);

            _objectPools.Add(obj);

            obj.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj = null;
        foreach(GameObject objs in _objectPools)
        {
            if(!objs.activeInHierarchy)
            {
                obj = objs;
                continue;
            }
        }
        return obj;
    }
}
