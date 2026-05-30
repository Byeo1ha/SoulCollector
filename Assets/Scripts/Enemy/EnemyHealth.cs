using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private EnemyDie enemyDie;

    private float currentHp;

    private void OnEnable()
    {
        currentHp = enemyData.maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
            return;

        currentHp -= damage;

        if (currentHp <= 0f)
        {
            enemyDie.Die();
        }
    }

    
}