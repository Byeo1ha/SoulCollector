using UnityEngine;

[RequireComponent(typeof(EnemyDie))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private EnemyDie enemyDie;
    private float currentHp;
    private float hpMultiplier = 1f;

    private void Awake()
    {
        enemyDie = GetComponent<EnemyDie>();
    }

    private void OnEnable()
    {
        currentHp = enemyData.maxHp * hpMultiplier;
    }

    public void SetHpMultiplier(float multiplier)
    {
        hpMultiplier = multiplier;
        currentHp = enemyData.maxHp * hpMultiplier;
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