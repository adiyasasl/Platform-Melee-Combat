using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private GameObject _objToSpawn;
    [SerializeField]
    private int _amountSpawn = 1;

    public void Spawn(Transform position)
    {
        int currentSpawn = 0;
        while(currentSpawn != _amountSpawn)
        {
            Instantiate(_objToSpawn, position.position, Quaternion.identity);
            currentSpawn++;
        }
    }
}
