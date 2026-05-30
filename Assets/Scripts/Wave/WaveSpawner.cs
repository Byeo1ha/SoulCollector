using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private int currentStage = 1;

    [SerializeField] private SpawnPoint spawnPointA;
    [SerializeField] private SpawnPoint spawnPointB;

    [SerializeField] private EnemyPool ghoulPool;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int enemyCount = WaveCalculator.GetEnemyCount(currentStage);
        float spawnInterval = WaveCalculator.GetSpawnInterval(currentStage);

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnPoint spawnPoint =
                Random.value < 0.5f ? spawnPointA : spawnPointB;

            spawnPoint.SpawnEnemy(ghoulPool);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}