using UnityEngine;

public class TurretSniper : TurretBase
{
    [SerializeField] private TurretSniperData turretSniperData;
    [SerializeField] private BulletSniperPool bulletSniperPool;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private bool isFind = false;

    private Transform _currentTarget;
    private float _nextFireTime = 0f;

    private void Update()
    {
        if (isFind) TryAttack();
    }

    private void TryAttack()
    {
        if (!IsTargetValid(_currentTarget, transform.position, turretSniperData.attackRange))
        {
            _currentTarget = FindNearestTarget(transform.position, turretSniperData.attackRange);
        }

        if (_currentTarget == null) return;

        Attack(_currentTarget);
    }

    private void Attack(Transform target)
    {
        if(Time.time < _nextFireTime) return;

        _nextFireTime = Time.time + turretSniperData.cooldown;
        
        GameObject bullet = bulletSniperPool.GetBullet();

        if(bullet == null) return;

        bullet.transform.position = shootPoint.position;
        
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.SetTarget(target, turretSniperData.shootingSpeed);

        bullet.SetActive(true);
    }
}
