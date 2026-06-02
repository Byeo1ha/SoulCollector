using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    public void Die()
    {
        GiveReward();
        RemoveEnemy();
    }

    public void ReachGoal()
    {
        if (PlayerBase.Instance != null)
        {
            PlayerBase.Instance.TakeDamage(enemyHealth.MaxHp);
        }

        RemoveEnemy();
    }

    private void GiveReward()
    {
        if (CurrencyManager.Instance == null)
            return;

        CurrencyManager.Instance.AddCrystal(enemyData.crystalReward);
    }

    private void RemoveEnemy()
    {
        WaveManager.Instance.UnregisterEnemy();
        gameObject.SetActive(false);
    }
}