using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameCanvasRotate : MonoBehaviour
{
    private Transform _enemyTransform;
    void Start()
    {
        _enemyTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyTransform.eulerAngles.y != -180)
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
}
