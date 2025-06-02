using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySystem : MonoBehaviour
{
    public static DummySystem Instance;

    [SerializeField]
    private DummyBehaviour[] _enemiesObj;
    [Range(1, 8)]
    [SerializeField]
    private int _rangeAppear = 3;

    [SerializeField]
    private List<DummyBehaviour> _activeObjects = new List<DummyBehaviour>();

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(StartAppearing());
    }
    public void AppearDummy()
    {
        // foreach(DummyBehaviour enemy in _enemiesObj)
        // {
        //     enemy.Appear();
        // }//
        int currentIndex = 0;

        while(currentIndex < _rangeAppear)
        {
            int randomized = Random.Range(0, _enemiesObj.Length);
            if(_enemiesObj[randomized].CanAppear)
            {
                _activeObjects.Add(_enemiesObj[randomized]);
                _enemiesObj[randomized].Appear();
                currentIndex++;
            }
        }

        StartCoroutine(StartDissapearing());
    }

    public void DisappearDummy()
    {
        for(int i = 0; i < _activeObjects.Count; i++)
        {
            _activeObjects[i].Dissapear();
        }
        _activeObjects.Clear();
        StartCoroutine(StartAppearing());
    }

    public void RemoveAppearObj(DummyBehaviour obj)
    {
        _activeObjects.Remove(obj);

        if(_activeObjects.Count == 0)
        {
            StopAllCoroutines();
            StartCoroutine(StartAppearing());
        }
    }

    private IEnumerator StartAppearing()
    {
        yield return new WaitForSeconds(1.5f);
        AppearDummy();
    }

    private IEnumerator StartDissapearing()
    {
        yield return new WaitForSeconds(4f);
        DisappearDummy();
    }
}
