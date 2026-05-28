using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxPool = 20;

    private readonly List<GameObject> pool = new();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            pool.Add(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        Debug.LogWarning($"{name} 풀에 사용 가능한 적이 없습니다.");
        return null;
    }
}