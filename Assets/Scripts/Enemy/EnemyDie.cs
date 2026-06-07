using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyDie : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float deathDelay = 0.8f;

    private EnemyHealth enemyHealth;
    private Animator animator;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        GiveReward();
        StartCoroutine(DieRoutine());
    }

    public void ReachGoal()
    {
        PlayerBase.Instance.TakeDamage(enemyHealth.MaxHp);
        RemoveEnemy();
    }

    private IEnumerator DieRoutine()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        yield return new WaitForSeconds(deathDelay);

        RemoveEnemy();
    }

    private void GiveReward()
    {
        if (CurrencyManager.Instance == null)
            return;

        CurrencyManager.Instance.AddSoul(enemyData.crystalReward);
    }

    private void RemoveEnemy()
    {
        WaveManager.Instance.UnregisterEnemy();
        gameObject.SetActive(false);
    }
}