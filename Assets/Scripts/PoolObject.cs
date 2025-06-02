using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private ObjectPooling _objectPooling;

    [Header("Properties")]
    [SerializeField]
    private Transform _objSpawnPosition;

    public void SpawnObjectPooling()
    {
        GameObject obj = _objectPooling.GetObject();

        obj.transform.position = _objSpawnPosition.position;
        obj.SetActive(true);
    }
}
