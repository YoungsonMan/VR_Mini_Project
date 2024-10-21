using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField] float SpawnTime;
    [SerializeField] GameObject[] spawnPoints;
    
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }
    void Update()
    {
        
    }


    IEnumerator EnemySpawn()
    {
        // Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        ObjectPoolingManager.SpawnObject(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(SpawnTime);

        StartCoroutine(EnemySpawn());
    }


}
