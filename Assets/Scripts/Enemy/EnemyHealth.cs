using UnityEngine;

[RequireComponent(typeof(EnemyDie))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public float MaxHp { get; private set; }

    private EnemyDie enemyDie;
    private float currentHp;
    private float hpMultiplier = 1f;

    private void Awake()
    {
        enemyDie = GetComponent<EnemyDie>();
    }

    private void OnEnable()
    {
    MaxHp = enemyData.maxHp * hpMultiplier;
    currentHp = MaxHp;
    }

    public void SetHpMultiplier(float multiplier)
    {
    hpMultiplier = multiplier;
    MaxHp = enemyData.maxHp * hpMultiplier;
    currentHp = MaxHp;
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