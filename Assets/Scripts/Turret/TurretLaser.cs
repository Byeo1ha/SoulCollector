using System.Collections;
using UnityEngine;

public class TurretLaser : TurretBase
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private LaserPool laserPool;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private bool isFind = true;

    private SpriteRenderer spriteRenderer;
    private TurretStat turretStat;
    private Transform _currentTarget;
    private float _nextFireTime = 0f;
    private bool _isAttackWaiting = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (Time.time < _nextFireTime) return;
        if (_isAttackWaiting) return;

        Vector3 currentPosition = transform.position;
        
        if (currentPosition.x < target.position.x) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;

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

        ShootLaser(target);
        _isAttackWaiting = false;
    }

    private void ShootLaser(Transform target)
    {
        GameObject laser = laserPool.GetLaser();

        if (laser == null) return;

        laser.transform.position = shootPoint.position;

        Vector2 direction = target.position - shootPoint.position;

        if (direction != Vector2.zero)
        {
            laser.transform.right = direction;
        }

        LaserProjectile laserProjectile = laser.GetComponent<LaserProjectile>();

        if (laserProjectile != null)
        {
            laserProjectile.SetDamage(turretStat.attackDamage);
        }

        laser.SetActive(true);
    }
}
