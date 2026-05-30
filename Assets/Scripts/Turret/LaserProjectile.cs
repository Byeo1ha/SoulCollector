using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;

    private readonly List<EnemyHealth> damagedEnemies = new List<EnemyHealth>();

    private float damage;

    private void OnEnable()
    {
        damagedEnemies.Clear();
        StartCoroutine(DisableAfterDelay());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        damagedEnemies.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDamage(collision);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void TryDamage(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth == null) return;
        if (damagedEnemies.Contains(enemyHealth)) return;

        enemyHealth.TakeDamage(damage);
        damagedEnemies.Add(enemyHealth);
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
}
