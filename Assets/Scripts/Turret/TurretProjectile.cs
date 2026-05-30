using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : TurretBase
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int maxBullets = 10;

    [SerializeField] private bool isFind = true;

    private readonly List<GameObject> bulletPool = new List<GameObject>();

    private TurretStat turretStat;
    private Transform _currentTarget;
    private float _nextFireTime = 0f;
    private bool _isAttackWaiting = false;

    private void Awake()
    {
        turretStat = turretData.RuntimeStat;
        CreateBulletPool();
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

    private void CreateBulletPool()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
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

        ShootBullet(target);
        _isAttackWaiting = false;
    }

    private void ShootBullet(Transform target)
    {
        GameObject bullet = GetBullet();

        if (bullet == null) return;

        bullet.transform.position = shootPoint.position;

        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.SetTarget(target, turretStat.shootingSpeed, turretStat.attackDamage);

        bullet.SetActive(true);
    }

    private GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeSelf)
            {
                return bulletPool[i];
            }
        }

        return null;
    }
}
