using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyPath enemyPath;

    public GameObject SpawnEnemy(EnemyPool enemyPool)
    {
        GameObject enemy = enemyPool.GetEnemy();

        if (enemy == null)
            return null;

        enemy.transform.position = transform.position;

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        if (movement != null)
        {
            movement.SetPath(enemyPath.GetWaypoints());
        }

        enemy.SetActive(true);

        return enemy;
    }
}