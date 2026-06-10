using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] private LaserHitEffectPool hitEffectPool;
    
    private readonly List<EnemyHealth> damagedEnemies = new List<EnemyHealth>();

    private float damage;

    private void OnEnable()
    {
        damagedEnemies.Clear();
        //StartCoroutine(DisableAfterDelay());
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
        Debug.Log(damage + " 피해 줌!");

        Vector3 hitPosition = collision.transform.position;

        PlayHitEffect(hitPosition);
        damagedEnemies.Add(enemyHealth);
    }

    /*private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }*/

    private void PlayHitEffect(Vector3 position)
    {
        Debug.Log("오긴 왔는데");
        if (hitEffectPool == null) return;


        Debug.Log("실행이 되네?");
        GameObject hitEffect = hitEffectPool.GetEffect();

        if (hitEffect == null) return;

        hitEffect.transform.position = position;
        hitEffect.SetActive(true);
    }

    public void OnDeActive()
    {
        gameObject.SetActive(false);
    }
}
