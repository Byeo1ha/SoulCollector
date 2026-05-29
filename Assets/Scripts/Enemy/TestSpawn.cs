using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private EnemyPath enemyPath;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetEnemy();

        if (enemy == null)
        {
            Debug.LogWarning("스폰 가능한 적이 없습니다.");
            return;
        }

        enemy.transform.position = transform.position;

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        movement.SetPath(enemyPath.GetWaypoints());

        enemy.SetActive(true);
    }
}
