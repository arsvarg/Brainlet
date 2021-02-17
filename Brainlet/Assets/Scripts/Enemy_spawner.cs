using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_spawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] float _spawnRate;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5, _spawnRate);
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-15f, 15f);
        float y = Random.Range(7f, 17f);
        Instantiate(_enemyPrefab, new Vector3(x, y, transform.position.z), Quaternion.identity);
    }
}
