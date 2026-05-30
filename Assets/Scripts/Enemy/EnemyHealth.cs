using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private EnemyDie enemyDie;

    private float currentHp;
    private void Awake()
    {
        enemyDie = GetComponent<EnemyDie>();
    }

    private void OnEnable()
    {
        currentHp = enemyData.maxHp;
    }

    public void TakeDamage(float _damage)
    {
        if (_damage <= 0f)
            return;

        currentHp -= _damage;

        if (currentHp <= 0f)
        {
            enemyDie.Die();
        }
    }

    
}