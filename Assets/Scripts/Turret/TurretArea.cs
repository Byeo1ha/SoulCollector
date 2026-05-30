using System.Collections;
using UnityEngine;

public class TurretArea : TurretBase
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private AreaHitEffectPool hitEffectPool;

    [SerializeField] private bool isFind = true;

    private TurretStat turretStat;
    private Transform _currentTarget;
    private float _nextFireTime = 0f;
    private bool _isAttackWaiting = false;

    private void Awake()
    {
        turretStat = turretData.RuntimeStat;
    }

    private void Update()
    {
        if (isFind) TryAttack();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
        _isAttackWaiting = false;
    }

    protected override void TryAttack()
    {
        _currentTarget = FindFirstTarget(transform.position, turretStat.attackRange);

        if (_currentTarget == null) return;

        Attack(_currentTarget);
    }

    private void Attack(Transform target)
    {
        if(Time.time < _nextFireTime) return;
        if (_isAttackWaiting) return;

        _nextFireTime = Time.time + turretStat.cooldown;

        StartCoroutine(AttackDelayCoroutine(target));
    }

    private IEnumerator AttackDelayCoroutine(Transform target)
    {
        _isAttackWaiting = true;

        if (turretStat.attackDelay > 0f)
        {
            yield return new WaitForSeconds(turretStat.attackDelay);
        }

        if (!IsTargetValid(target, transform.position, turretStat.attackRange))
        {
            _isAttackWaiting = false;
            yield break;
        }

        ApplyDamage(target);
        _isAttackWaiting = false;
    }

    private void ApplyDamage(Transform target)
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();

        if (enemyHealth == null) return;

        Vector3 hitPosition = target.position;
        enemyHealth.TakeDamage(turretStat.attackDamage);
        PlayHitEffect(hitPosition);
    }

    private void PlayHitEffect(Vector3 position)
    {
        if (hitEffectPool == null) return;

        GameObject hitEffect = hitEffectPool.GetEffect();

        if (hitEffect == null) return;

        hitEffect.transform.position = position;
        hitEffect.SetActive(true);
    }
}
