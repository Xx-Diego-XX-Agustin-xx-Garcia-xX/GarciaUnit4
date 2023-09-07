using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9;
    void Start()
    {
        int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[randomPowerUp], GenerateSpawnPosition(), powerUpPrefabs[randomPowerUp].transform.rotation);
        SpawnEnemyWave(waveNumber);
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
        }
    }
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomPowerUp], GenerateSpawnPosition(), powerUpPrefabs[randomPowerUp].transform.rotation);
        }
    }
}
