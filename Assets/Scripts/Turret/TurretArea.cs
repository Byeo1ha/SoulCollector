using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TurretSound))]
public class TurretArea : TurretBase
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private AreaHitEffectPool hitEffectPool;

    [SerializeField] private bool isFind = true;

    private ITurretAttackAnim _turretAttackAnim;
    private TurretSound turretSound;
    private TurretStat turretStat;
    private Transform _currentTarget;

    private float _originalScaleXValue;
    private float _nextFireTime = 0f;
    private bool _isAttackWaiting = false;

    public override int Cost => turretStat.cost;

    private void Awake()
    {
        _turretAttackAnim = GetComponentInChildren<ITurretAttackAnim>();
        turretSound = GetComponent<TurretSound>();
        _originalScaleXValue = transform.localScale.x;
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

        LookAtTarget(target);

        _nextFireTime = Time.time + turretStat.cooldown;

        _turretAttackAnim.OnAttackAnimation();
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
            target = FindFirstTarget(transform.position, turretStat.attackRange);

            if (target == null)
            {
                _isAttackWaiting = false;
                yield break;
            }

            LookAtTarget(target);
        }

        if (!isFind)
        {
            _isAttackWaiting = false;
            yield break;
        }

        turretSound.PlaySoundAttack();
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

    private void LookAtTarget(Transform target)
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x < target.position.x)
        {
            transform.localScale = new Vector3(_originalScaleXValue, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(_originalScaleXValue * (-1), transform.localScale.y, transform.localScale.z);
        }
    }

    private void PlayHitEffect(Vector3 position)
    {
        if (hitEffectPool == null) return;

        GameObject hitEffect = hitEffectPool.GetEffect();

        if (hitEffect == null) return;

        hitEffect.transform.position = position;
        hitEffect.SetActive(true);
    }

    public void SetFind(bool value)
    {
        isFind = value;
    }
}
