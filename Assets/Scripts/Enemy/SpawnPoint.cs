using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyPath enemyPath;

    public void SpawnEnemy(EnemyPool enemyPool)
    {
        GameObject enemy = enemyPool.GetEnemy();

        if (enemy == null)
            return;

        enemy.transform.position = transform.position;

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.SetPath(enemyPath.GetWaypoints());

        enemy.SetActive(true);
    }
}