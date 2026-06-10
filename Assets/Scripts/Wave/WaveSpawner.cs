using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private int currentStage = 1;

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private PlayerBase playerBase;
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField] private SpawnPoint spawnPoint;

    [SerializeField] private EnemyPool ghoulPool;
    [SerializeField] private EnemyPool soulEaterPool;
    [SerializeField] private EnemyPool crystalGolemPool;
    [SerializeField] private EnemyPool dropperPool;
    [SerializeField] private EnemyPool guardianPool;

    public IEnumerator SpawnWave(int stage)
    {
        waveManager.SetSpawningState(true);

        int enemyCount = WaveCalculator.GetEnemyCount(stage);
        float spawnInterval = WaveCalculator.GetSpawnInterval(stage);
        float hpMultiplier = stage >= 11 ? 2f : 1f;

        List<EnemyPool> spawnList = CreateSpawnList(enemyCount, stage);

        for (int i = 0; i < spawnList.Count; i++)
        {
            if (waveManager.IsGameEnded)
                yield break;

            SpawnEnemy(spawnPoint, spawnList[i], hpMultiplier);

            yield return new WaitForSeconds(spawnInterval);
        }

        waveManager.SetSpawningState(false);
    }

    private void SpawnEnemy(SpawnPoint spawnPoint, EnemyPool enemyPool, float hpMultiplier)
    {
        GameObject enemy = spawnPoint.SpawnEnemy(enemyPool);

        if (enemy == null)
            return;

        EnemyDie enemyDie = enemy.GetComponent<EnemyDie>();

        if (enemyDie != null)
        {
            enemyDie.Initialize(playerBase, currencyManager, waveManager);
        }

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.SetHpMultiplier(hpMultiplier);
        }

        DropperSkill dropperSkill = enemy.GetComponent<DropperSkill>();

        if (dropperSkill != null)
        {
            dropperSkill.Initialize(ghoulPool, waveManager);
        }

        waveManager.RegisterEnemy();
    }

    private float GetHpMultiplier()
    {
        return currentStage >= 11 ? 2f : 1f;
    }

    private List<EnemyPool> CreateSpawnList(int enemyCount, int stage)
    {
        List<EnemyPool> result = new();

        EnemyPool bossPool = null;

        if (stage == 10)
        {
            enemyCount -= 1;
            bossPool = dropperPool;
            AddThreeTypeEnemies(result, enemyCount);
        }
        else if (stage == 20)
        {
            enemyCount -= 1;
            bossPool = guardianPool;
            AddThreeTypeEnemies(result, enemyCount);
        }
        else if (stage <= 3)
        {
            AddEnemies(result, ghoulPool, enemyCount);
        }
        else if (stage <= 6)
        {
            AddTwoTypeEnemies(result, enemyCount);
        }
        else
        {
            AddThreeTypeEnemies(result, enemyCount);
        }

        Shuffle(result);

        if (bossPool != null)
        {
            result.Add(bossPool);
        }

        return result;
    }

    private void AddTwoTypeEnemies(List<EnemyPool> list, int enemyCount)
    {
        int baseCount = enemyCount / 2;
        int remainder = enemyCount % 2;

        AddEnemies(list, ghoulPool, baseCount);
        AddEnemies(list, soulEaterPool, baseCount);

        if (remainder == 1)
        {
            EnemyPool randomPool = Random.value < 0.5f ? ghoulPool : soulEaterPool;
            list.Add(randomPool);
        }
    }

    private void AddThreeTypeEnemies(List<EnemyPool> list, int enemyCount)
    {
        int baseCount = enemyCount / 3;
        int remainder = enemyCount % 3;

        AddEnemies(list, ghoulPool, baseCount);
        AddEnemies(list, soulEaterPool, baseCount);
        AddEnemies(list, crystalGolemPool, baseCount);

        if (remainder == 1)
        {
            EnemyPool randomPool = Random.value < 0.5f ? ghoulPool : soulEaterPool;
            list.Add(randomPool);
        }
        else if (remainder == 2)
        {
            list.Add(ghoulPool);
            list.Add(soulEaterPool);
        }
    }

    private void AddEnemies(List<EnemyPool> list, EnemyPool pool, int count)
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(pool);
        }
    }

    private void Shuffle(List<EnemyPool> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}