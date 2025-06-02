using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _obj;

    [SerializeField]
    private UnityEvent _onObjectDestroy;
    public void DestroyObjEnemy()
    {
        InvokeEvent();
        HideObject();
        
        WaveSystem.Instance.CheckEnemy();

        DestroyObj();
    }

    public void DestroyObj()
    {
        Destroy(_obj);
    }

    public void HideObj()
    {
        InvokeEvent();

        HideObject();
    }

    private void HideObject()
    {
        _obj.SetActive(false);
    }

    public void InvokeEvent()
    {
        if (_onObjectDestroy != null)
            _onObjectDestroy.Invoke();
    }
}
