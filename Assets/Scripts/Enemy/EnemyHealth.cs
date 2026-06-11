using UnityEngine;

[RequireComponent(typeof(EnemyDie))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public float MaxHp { get; private set; }

    private EnemyDie enemyDie;
    private float currentHp;
    private float hpMultiplier = 1f;
    private DropperSkill dropperSkill;

    private void Awake()
    {
        enemyDie = GetComponent<EnemyDie>();
        dropperSkill = GetComponent<DropperSkill>();
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

    if (enemyDie.IsDead)
        return;

    currentHp -= damage;

    if (dropperSkill != null)
    {
        dropperSkill.TrySummon(currentHp, MaxHp);
    }

    if (currentHp <= 0f)
    {
        enemyDie.Die();
    }
}
}
