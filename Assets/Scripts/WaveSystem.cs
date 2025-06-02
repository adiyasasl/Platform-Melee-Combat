using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyName
{
    SKELETON,
    FLY_MONSTER
}

[Serializable]
public class EnemyInfo
{
    public EnemyName _enemyName;
    [Range(1f, 100f)]
    public float _rateSpawn;
    public GameObject _enemyObject;
}
public class WaveSystem : MonoBehaviour
{
    public static WaveSystem Instance;

    [Header("Properties")]
    [SerializeField]
    private GameObject _shopPanel;
    [SerializeField]
    private GameObject _waveDisplay;
    [SerializeField]
    private List<EnemyInfo> _enemyObject = new List<EnemyInfo>();
    [SerializeField]
    private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField]
    private List<GameObject> _enemiesObjects = new List<GameObject>();
    [SerializeField]
    private int _firstTargetSpawn = 5;

    private int _currentSpawn = 0;
    private bool _canSpawn = true;
    public int _currentWave = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //_waveDisplay.SetActive(true);
    }

    void Update()
    {
        //debugOnly
        if (Input.GetKeyDown(KeyCode.T))
            _waveDisplay.SetActive(true);
    }

    public void CheckEnemy()
    {
        for(int i = 0; i < _enemiesObjects.Count; i++)
        {
            GameObject enemy = _enemiesObjects[i];
            if (!enemy.activeInHierarchy)
                _enemiesObjects.RemoveAt(i);
        }

        if (_enemiesObjects.Count == 0 && _canSpawn)
        {
            Debug.Log("There's no enemies!");
            _canSpawn = false;
            StartSpawn();
        }
        else if (_enemiesObjects.Count == 0)
        {
            _shopPanel.SetActive(true);
        }

    }

    public void StartSpawn()
    {
        int enemiesCount = _firstTargetSpawn * _currentWave;

        if(_currentSpawn != enemiesCount)
        {
            GameObject getEnemy = null;
            if(_currentWave < 2)
            {
                foreach (EnemyInfo obj in _enemyObject)
                {
                    if(obj._enemyName == EnemyName.SKELETON)
                    {
                        getEnemy = obj._enemyObject;
                        continue;
                    }
                }
            }
            else
            {
                GameObject defaultEnemy = null;
                foreach (EnemyInfo obj in _enemyObject)
                {
                    float random = UnityEngine.Random.Range(1, 100);
                    

                    if(obj._enemyName == EnemyName.SKELETON)
                    {
                        defaultEnemy = obj._enemyObject;
                        continue;
                    }

                    if(random <= obj._rateSpawn && obj._enemyName == EnemyName.FLY_MONSTER)
                    {
                        getEnemy = obj._enemyObject;
                        continue;
                    }
                }

                if (getEnemy == null)
                    getEnemy = defaultEnemy;
            }
            GameObject enemy = Instantiate(getEnemy, _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
            //GameObject enemy = Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
            AddEnemyObject(enemy);

            StartCoroutine(DelaySpawn());
            _currentSpawn++;
        }
        else
        {
            _currentSpawn = 0;
            _canSpawn = false;
            return;
        }
        
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3f);
        StartSpawn();
    }

    private void AddEnemyObject(GameObject enemy)
    {
        _enemiesObjects.Add(enemy);
    }

    public void DeleteEnemy(GameObject enemy)
    {
        _enemiesObjects.Remove(enemy);
        CheckEnemy();
    }

    public void ActiveSpawn()
    {
        _canSpawn = true;
        _currentWave++;
        CheckEnemy();
    }
}
